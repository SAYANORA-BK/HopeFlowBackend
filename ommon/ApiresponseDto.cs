﻿namespace ommon
{
    public class ApiresponseDto<T>
    {
       public int StatusCode {  get; set; }
        public string? Message { get; set; }
        public T? Data {  get; set; }
    }
}
