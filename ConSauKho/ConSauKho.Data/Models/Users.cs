using System;
using System.Collections.Generic;

namespace ConSauKho.Data.Models
{
    public partial class Users
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Salt { get; set; }
    }
}
