using System;

namespace FaceIn.Common
{
    
    public interface IFlowCommand
    {
        
        event Action ExecuteAction;
                 
    }

    public interface IFlowParamCommand
    {
         event ParamAction ExecuteAction;
    }

    public delegate void ParamAction(object value);
}
