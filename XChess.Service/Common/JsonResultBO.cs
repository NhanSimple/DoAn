﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Service.Common
{
    public class JsonResultBO
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Param { get; set; }
        public string RedirectUrl { get; set; }
        public JsonResultBO(bool st)
        {
            Status = st;
        }
        public JsonResultBO(bool st, string message)
        {
            Status = st;
            Message= message;
        }
        public void MessageFail(string mss)
        {
            Status = false;
            Message = mss;
        }
        public void MessageSuccess(string mss)
        {
            Status = true;
            Message = mss;
        }
    }
}
