using System;
using System.Transactions;

namespace Evolutional.Project.CrossCutting.Configuration
{
    public static class Transaction
    {
        public static TransactionScope GetTransaction()
        {
            return GetTransaction(TransactionManager.DefaultTimeout, IsolationLevel.ReadCommitted);
        }

        public static TransactionScope GetTransaction(TimeSpan timeout)
        {
            return GetTransaction(timeout, IsolationLevel.ReadCommitted);
        }

        public static TransactionScope GetTransaction(IsolationLevel isolationLevel)
        {
            return GetTransaction(TransactionManager.DefaultTimeout, isolationLevel);
        }

        public static TransactionScope GetTransaction(TransactionScopeOption opt)
        {
            return GetTransaction(TransactionManager.DefaultTimeout, IsolationLevel.ReadCommitted, opt);
        }

        public static TransactionScope GetTransaction(
                TimeSpan timeout,
                IsolationLevel isolationLevel,
                TransactionScopeOption opt = TransactionScopeOption.Required
            )
        {
            var option = new TransactionOptions
            {
                IsolationLevel = isolationLevel,
                Timeout = timeout
            };
            return new TransactionScope(opt, option, TransactionScopeAsyncFlowOption.Enabled);
        }

        public static TransactionScope GetTransactionAsync(int TimeoutPadrao)
        {
            return new TransactionScope(
                           TransactionScopeOption.Required,
                           new TransactionOptions()
                           {
                               IsolationLevel = IsolationLevel.ReadCommitted,
                               Timeout = TimeSpan.FromSeconds(TimeoutPadrao)
                           },
                           TransactionScopeAsyncFlowOption.Enabled
                       );
        }
    }
}
