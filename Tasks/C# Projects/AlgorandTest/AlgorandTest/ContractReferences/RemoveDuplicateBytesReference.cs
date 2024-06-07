﻿using AlgoStudio.Core;
using AlgoStudio.Core.Attributes;
using System;

namespace AlgorandTest.ContractReferences
{
    public abstract class RemoveDuplicateBytesReference : SmartContractReference
    {

        [SmartContractMethod(OnCompleteType.NoOp, "Dedup")]
        public abstract ValueTuple<AppCall> Dedup(byte[] inputString, out byte[] result);


    }
}