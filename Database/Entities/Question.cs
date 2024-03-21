using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banq.Database.Entities
{
    public class Question
    {
        public ulong Id { get; set; }
        public virtual QuestionSet QuestionSet { get; set; }
        public string Content { get; set; }
    }
}