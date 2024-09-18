﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.DataBase.Entities {
    public class RefreshToken {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
