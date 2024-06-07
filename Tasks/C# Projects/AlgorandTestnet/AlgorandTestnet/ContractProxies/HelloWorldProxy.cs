using System;
using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using AlgoStudio;
using AlgoStudio.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxies
{

	
	public class HelloWorldProxy : ProxyBase
	{
		
		public HelloWorldProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) 
		{
		}

		///<summary>
        ///
        ///No_op: CALL, Opt_in: NEVER, Close_out: NEVER, Update_application: NEVER, Delete_application: NEVER
        ///</summary>
		public async Task<string> Helloworld (Account sender, ulong? fee,string note,  List<BoxRef> boxes, AlgoStudio.Core.OnCompleteType callType = AlgoStudio.Core.OnCompleteType.NoOp )
		{
			byte[] abiHandle = {72,101,108,108,111};
			var result = await base.CallApp(null, fee, callType, 1000, note, sender,  new List<object> {abiHandle}, null, null,null, boxes);
			return Encoding.UTF8.GetString(result.First());

		}

		public async Task<List<Transaction>> Helloworld_Transactions (Account sender, ulong? fee,string note, List<BoxRef> boxes, AlgoStudio.Core.OnCompleteType callType = AlgoStudio.Core.OnCompleteType.NoOp )
		{
			byte[] abiHandle = {72,101,108,108,111};
			return await base.MakeTransactionList(null, fee, callType, 1000, note, sender,  new List<object> {abiHandle}, null, null,null,boxes);

		}

		///<summary>
        ///
        ///No_op: CALL, Opt_in: NEVER, Close_out: NEVER, Update_application: NEVER, Delete_application: NEVER
        ///</summary>
		/// <param name="a"> Type is int  </param>
		/// <param name="b"> Type is int  </param>
		public async Task<int> Addition (Account sender, ulong? fee,int a,int b,string note,  List<BoxRef> boxes, AlgoStudio.Core.OnCompleteType callType = AlgoStudio.Core.OnCompleteType.NoOp )
		{
			byte[] abiHandle = {83,117,109};
			var result = await base.CallApp(null, fee, callType, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null, boxes);
			return BitConverter.ToInt32(ReverseIfLittleEndian(result.First().ToArray()), 0);

		}

		public async Task<List<Transaction>> Addition_Transactions (Account sender, ulong? fee, int a,int b,string note, List<BoxRef> boxes, AlgoStudio.Core.OnCompleteType callType = AlgoStudio.Core.OnCompleteType.NoOp )
		{
			byte[] abiHandle = {83,117,109};
			return await base.MakeTransactionList(null, fee, callType, 1000, note, sender,  new List<object> {abiHandle,a,b}, null, null,null,boxes);

		}

	}

}
