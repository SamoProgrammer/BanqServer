using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banq.Database.Entities;

namespace Banq.ViewModels
{
    public class QuestionViewModel
    {
        public ulong Id { get; set; }
        public QuestionSet QuestionSet { get; set; }
        public string Content { get; set; }
    }
}