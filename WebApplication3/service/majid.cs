using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.service
{
    public class Foo : IFoo,IBaseService
    {
        public string Hello()
        {
            return "Hello";
        }
    }

  //  public abstract class BaseService : IBaseService { }


}
