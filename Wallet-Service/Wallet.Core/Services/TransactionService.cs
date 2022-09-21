using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Core.Interfaces;
using Wallet.Data;
using Wallet.Dtos;
using Wallet.Dtos.Pagination;
using Wallet.Model;
using Wallet.Model.Enums;
namespace Wallet.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public TransactionService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task CreateFundingTransactionAsync(string txRef, string amount, int userId)
        {
            var transaction = new UserTransaction();
            var userWalletId = GetWalletId(userId);
            transaction.Amount = amount;
            transaction.TransactionReference = txRef;
            transaction.WalletId = userWalletId;
            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync();
        }
        public async Task<ExecutionResponse<UserTransactionDto>> CreateWalletToWalletTransactionAsync(int senderWalletId,
            int receiverWalletId, int amount, string description)
        {
            if (amount <= 0)
                return new ExecutionResponse<UserTransactionDto>()
                {
                    Status = false,
                    Message = "Invalid amount",
                    StatusCode = 400,
                    Data = null
                };
            var senderWallet = await GetWallet(senderWalletId);
            var receiverWallet = await GetWallet(receiverWalletId);
            if (WalletCanTransact(senderWallet) && WalletCanTransact(receiverWallet))
            {
                if (amount > senderWallet.Balance)
                    return new ExecutionResponse<UserTransactionDto>()
                    {
                        Status = false,
                        Message = "Insufficient balance",
                        StatusCode = 400,
                        Data = null
                    };
                senderWallet.Balance -= amount;
                receiverWallet.Balance += amount;
                var senderTransaction = CreateTransactionWallet(senderWalletId, receiverWalletId, description, amount,
                    TransactionType.TransferingToWallet);
                _db.Transactions.Add(senderTransaction);
                _db.Transactions.Add(CreateTransactionWallet(receiverWalletId, senderWalletId, description, amount,
                    TransactionType.Receiving));
                await _db.SaveChangesAsync();
                var senderTransactionDto = (UserTransactionDto)_mapper.Map(senderTransaction, typeof(UserTransaction), typeof(UserTransactionDto));
                return new ExecutionResponse<UserTransactionDto>()
                {
                    Status = true,
                    Message = "Transaction Successful",
                    StatusCode = 200,
                    Data = senderTransactionDto
                };
            }
            return new ExecutionResponse<UserTransactionDto>()
            {
                Status = false,
                Message = "Transaction cannot be completed",
                StatusCode = 400,
                Data = null
            };
        }
        public Task<UserTransaction> GetTransactionAsync(string txRef) =>
            _db.Transactions.FirstOrDefaultAsync(x => x.TransactionReference.Equals(txRef));
        public async Task UpdateTransaction(UserTransaction transaction)
        {
            _db.Transactions.Update(transaction);
            await _db.SaveChangesAsync();
        }
        public async Task<PagedExecutionResponse<IEnumerable<UserTransactionDto>>> GetTransactionsForWallet(int id, int pageNumber, int pageSize, DateTime searchDate)
        {
            IQueryable<UserTransaction> transactions;
            if (searchDate == DateTime.MinValue)
            {
                transactions = _db.Transactions.Where(x => x.WalletId == id)
                .OrderBy(x => x.Created);
            }
            else
            {
                try
                {
                    transactions = _db.Transactions.Where(x => x.WalletId == id && x.Created.Date.Equals(searchDate.Date))
                    .OrderBy(x => x.Created);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw new Exception();
                }
            }
            var pagedTransactions = PagedList<UserTransaction>.ToPagedList(transactions, pageNumber, pageSize);
            var pagedTransactionDtos = (IEnumerable<UserTransactionDto>)_mapper.Map(pagedTransactions, typeof(IEnumerable<UserTransaction>),
                typeof(IEnumerable<UserTransactionDto>));
            return new PagedExecutionResponse<IEnumerable<UserTransactionDto>>()
            {
                Status = true,
                Message = null,
                StatusCode = 200,
                TotalRecords = pagedTransactions.TotalCount,
                CurrentPage = pagedTransactions.CurrentPage,
                PageSize = pagedTransactions.PageSize,
                Data = pagedTransactionDtos
            };
        }
        private int GetWalletId(int userId) =>
            _db.Wallets.FirstOrDefault(x => x.UserId == userId).Id;
        private async Task<UserWallet> GetWallet(int walletId) =>
            await _db.Wallets.FirstOrDefaultAsync(x => x.Id.Equals(walletId));
        private bool WalletCanTransact(UserWallet wallet)
        {
            if (wallet == null || !wallet.Status)
                return false;
            return true;
        }
        private UserTransaction CreateTransactionWallet(int userWalletId,
            int senderOrReceiverWalletId, string description, int amount, TransactionType type) =>
            new UserTransaction()
            {
                Status = TransactionStatus.Success,
                Type = type,
                Amount = amount.ToString(),
                WalletId = userWalletId,
                SenderOrReceiverWalletId = senderOrReceiverWalletId,
                Description = description,
                Created = DateTime.Now,
                Updated = DateTime.Now,
            };
    }
}