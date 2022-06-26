using System;

namespace TestMaker.EventService.Domain.Models
{
    public class SelectOption<T>
    {
        public T Value { get; set; }

        public string Title { get; set; }
    }
}
