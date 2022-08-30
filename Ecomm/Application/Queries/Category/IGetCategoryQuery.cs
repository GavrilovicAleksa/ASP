using Application.DataTransfer.Category;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Category
{ 
    public interface IGetCategoryQuery : IQuery<CategorySearch, SingleResponse<GetCategoryDto>>
    { 
    }
}
