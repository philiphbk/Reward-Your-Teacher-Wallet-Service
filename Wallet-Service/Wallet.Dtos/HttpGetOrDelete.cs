using System;
using System.Collections.Generic;
using System.Text;
using Wallet.Model.Enums;

namespace Wallet.Dtos
{
    public class HttpGetOrDelete
    {

        private ApiHttpVerbs _apiHttpVerbs;

        public string QueryString { get; set; }
        public string BaseUrl { get; set; }
        public string EndPoint { get; set; }
        public List<CustomHeader> CustomHeaders { get; set; }
        public ApiHttpVerbs ApiHttpVerbs
        {
            get
            {
                return (_apiHttpVerbs == 0) ? ApiHttpVerbs.Get : _apiHttpVerbs;
            }
            set
            {
                _apiHttpVerbs = value;
            }
        }
    }
}
