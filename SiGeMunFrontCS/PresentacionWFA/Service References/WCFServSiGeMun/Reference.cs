﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PresentacionWFA.WCFServSiGeMun {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WCFServSiGeMun.IWcfServSiGeMun")]
    public interface IWcfServSiGeMun {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfServSiGeMun/InsertScript", ReplyAction="http://tempuri.org/IWcfServSiGeMun/InsertScriptResponse")]
        long InsertScript(System.IO.Stream file);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfServSiGeMun/InsertScript", ReplyAction="http://tempuri.org/IWcfServSiGeMun/InsertScriptResponse")]
        System.Threading.Tasks.Task<long> InsertScriptAsync(System.IO.Stream file);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWcfServSiGeMunChannel : PresentacionWFA.WCFServSiGeMun.IWcfServSiGeMun, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WcfServSiGeMunClient : System.ServiceModel.ClientBase<PresentacionWFA.WCFServSiGeMun.IWcfServSiGeMun>, PresentacionWFA.WCFServSiGeMun.IWcfServSiGeMun {
        
        public WcfServSiGeMunClient() {
        }
        
        public WcfServSiGeMunClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WcfServSiGeMunClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WcfServSiGeMunClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WcfServSiGeMunClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public long InsertScript(System.IO.Stream file) {
            return base.Channel.InsertScript(file);
        }
        
        public System.Threading.Tasks.Task<long> InsertScriptAsync(System.IO.Stream file) {
            return base.Channel.InsertScriptAsync(file);
        }
    }
}