﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Infrastructure.EmailSender
{
    public class VerificationCodeInfo
    {
        public string Code { get; set; }
        public DateTime GeneratedAt { get; set; }
    }
}
