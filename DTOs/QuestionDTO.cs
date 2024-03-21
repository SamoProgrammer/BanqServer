using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banq.DTOs
{
    public class QuestionDTO
    {
        public ulong QuestionSetId { get; set; }
        public string Content { get; set; }
    }
}