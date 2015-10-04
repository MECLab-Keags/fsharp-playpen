using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing.CSharp
{
    [TestClass]
    public class HttpProxyTest
    {
        [TestMethod]
        public async Task GetAsync_Test()
        {
            var proxy = new HttpProxyHelper.HttpProxy();
            var target = await proxy.GetAsync("http://google.com");
            //var targetLazy = proxy.GetAsyncLazy.WithRetry();

            Assert.IsFalse(string.IsNullOrEmpty(target));
            //Assert.IsFalse(string.IsNullOrEmpty(targetLazy));
        }

        enum State { New, Draft, Published, Inactive, Discontinued }
        void HandleState(State state)
        {
            switch (state)
            {
                case State.Inactive: // code for Inactive
                    break;
                case State.Draft: // code for Draft
                    break;
                case State.New: // code for New
                    break;
                case State.Discontinued: // code for Discontinued
                    break;
            }
        }


    }
}
