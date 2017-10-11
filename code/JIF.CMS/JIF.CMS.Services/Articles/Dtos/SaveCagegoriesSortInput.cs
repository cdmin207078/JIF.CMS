using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Services.Articles.Dtos
{
    public class SaveCagegoriesSortInput
    {
        public int Id { get; set; }

        public List<SaveCagegoriesSortInput> Children { get; set; }
    }
}
