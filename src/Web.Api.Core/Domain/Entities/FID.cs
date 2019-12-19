using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Stateless;
using Web.Api.Core.Shared;

namespace Web.Api.Core.Domain.Entities
{
	public class FID : BaseEntity
	{
        public enum FIDTriggers
        {
        	Publish,
        	Cancel,
        	Request_Changes,
            Suspend,
            Terminate,
            Approve,
            Reject
        }
        public enum EvaluateState
        {
            Draft,
            Deleted,
            Awaiting_Review,
            Change_Needed,
            Reviewed,
            Awaiting_Evaluate,
            Approved,
            Rejected,
            Canceled
        }
        public EvaluateState State => _stateMachine.State;
        private readonly StateMachine<EvaluateState, FIDTriggers> _stateMachine;
        public string Name { get; }

        public FID(string name)
        {
        	_stateMachine = new StateMachine<EvaluateState, FIDTriggers>(EvaluateState.Draft);
            Name = name;

            ConfigureStateMachine();
        }

        [JsonConstructor]
        public FID(string state, string name)
        {
            var fidState = (EvaluateState) Enum.Parse(typeof(EvaluateState), state);
            _stateMachine = new StateMachine<EvaluateState, FIDTriggers>(fidState);
            Name = name;

            ConfigureStateMachine();
        }

        private void ConfigureStateMachine()
        {
            _stateMachine.Configure(EvaluateState.Draft)
                .Permit(FIDTriggers.Terminate, EvaluateState.Deleted)
                .Permit(FIDTriggers.Publish, EvaluateState.Awaiting_Review);

            _stateMachine.Configure(EvaluateState.Awaiting_Review)
                .Permit(FIDTriggers.Cancel, EvaluateState.Canceled)
                .Permit(FIDTriggers.Request_Changes, EvaluateState.Change_Needed)
                .Permit(FIDTriggers.Approve, EvaluateState.Reviewed);

            _stateMachine.Configure(EvaluateState.Change_Needed)
                .Permit(FIDTriggers.Publish, EvaluateState.Awaiting_Review);

            _stateMachine.Configure(EvaluateState.Reviewed)
                .Permit(FIDTriggers.Approve, EvaluateState.Awaiting_Evaluate)
                .Permit(FIDTriggers.Cancel, EvaluateState.Canceled);

            _stateMachine.Configure(EvaluateState.Awaiting_Evaluate)
                .Permit(FIDTriggers.Approve, EvaluateState.Approved)
                .Permit(FIDTriggers.Reject, EvaluateState.Rejected)
                .Permit(FIDTriggers.Cancel, EvaluateState.Canceled);
        }

        public void Terminate(){
        	_stateMachine.Fire(FIDTriggers.Terminate);
        }

        public void Publish()
        {
        	_stateMachine.Fire(FIDTriggers.Publish);
        }

        public void Approve()
        {
        	_stateMachine.Fire(FIDTriggers.Approve);
        }

        public void Cancel()
        {
        	_stateMachine.Fire(FIDTriggers.Cancel);
        }

        public void Reject()
        {
        	_stateMachine.Fire(FIDTriggers.Reject);
        }

        public void RequestChanges()
        {
        	_stateMachine.Fire(FIDTriggers.Request_Changes);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static FID FromJson(string jsonString)
        {
            return JsonConvert.DeserializeObject<FID>(jsonString);
        }
	}
}