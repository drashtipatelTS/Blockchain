using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorandTest.Contracts
{
    public class HelloWorld : SmartContract
    {
        protected override int ApprovalProgram(in AppCallTransactionReference transaction)
        {
            InvokeSmartContractMethod();
            return 1;
        }

        protected override int ClearStateProgram(in AppCallTransactionReference transaction)
        {
            return 1;
        }

        [SmartContractMethod(OnCompleteType.NoOp, "Hello")]
        public string Helloworld(AppCallTransactionReference current)
        {
            return "HelloWorld";
        }
        [SmartContractMethod(OnCompleteType.NoOp, "Sum")]
        public int Addition(int a, int b, AppCallTransactionReference current)
        {
            var sum = 0;
            sum = a + b;
            return sum;
        }
    }
}
