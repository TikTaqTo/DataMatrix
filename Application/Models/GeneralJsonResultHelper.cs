﻿namespace Application.Models
{
    public class GeneralJsonResultHelper<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
