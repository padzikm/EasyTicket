﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EasyTicketWPFClient.ServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/CheckNewOffer", ReplyAction="http://tempuri.org/IService/CheckNewOfferResponse")]
        bool CheckNewOffer();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/CheckNewOffer", ReplyAction="http://tempuri.org/IService/CheckNewOfferResponse")]
        System.Threading.Tasks.Task<bool> CheckNewOfferAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/SendMailToAll", ReplyAction="http://tempuri.org/IService/SendMailToAllResponse")]
        void SendMailToAll(string body);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/SendMailToAll", ReplyAction="http://tempuri.org/IService/SendMailToAllResponse")]
        System.Threading.Tasks.Task SendMailToAllAsync(string body);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/RegisteredUsers", ReplyAction="http://tempuri.org/IService/RegisteredUsersResponse")]
        int RegisteredUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/RegisteredUsers", ReplyAction="http://tempuri.org/IService/RegisteredUsersResponse")]
        System.Threading.Tasks.Task<int> RegisteredUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/RegisterPreferences", ReplyAction="http://tempuri.org/IService/RegisterPreferencesResponse")]
        void RegisterPreferences(EasyTicketLogic.User user, EasyTicketLogic.UserPreferences userPrefs);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/RegisterPreferences", ReplyAction="http://tempuri.org/IService/RegisterPreferencesResponse")]
        System.Threading.Tasks.Task RegisterPreferencesAsync(EasyTicketLogic.User user, EasyTicketLogic.UserPreferences userPrefs);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/CheckOffers", ReplyAction="http://tempuri.org/IService/CheckOffersResponse")]
        void CheckOffers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/CheckOffers", ReplyAction="http://tempuri.org/IService/CheckOffersResponse")]
        System.Threading.Tasks.Task CheckOffersAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : EasyTicketWPFClient.ServiceReference.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<EasyTicketWPFClient.ServiceReference.IService>, EasyTicketWPFClient.ServiceReference.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool CheckNewOffer() {
            return base.Channel.CheckNewOffer();
        }
        
        public System.Threading.Tasks.Task<bool> CheckNewOfferAsync() {
            return base.Channel.CheckNewOfferAsync();
        }
        
        public void SendMailToAll(string body) {
            base.Channel.SendMailToAll(body);
        }
        
        public System.Threading.Tasks.Task SendMailToAllAsync(string body) {
            return base.Channel.SendMailToAllAsync(body);
        }
        
        public int RegisteredUsers() {
            return base.Channel.RegisteredUsers();
        }
        
        public System.Threading.Tasks.Task<int> RegisteredUsersAsync() {
            return base.Channel.RegisteredUsersAsync();
        }
        
        public void RegisterPreferences(EasyTicketLogic.User user, EasyTicketLogic.UserPreferences userPrefs) {
            base.Channel.RegisterPreferences(user, userPrefs);
        }
        
        public System.Threading.Tasks.Task RegisterPreferencesAsync(EasyTicketLogic.User user, EasyTicketLogic.UserPreferences userPrefs) {
            return base.Channel.RegisterPreferencesAsync(user, userPrefs);
        }
        
        public void CheckOffers() {
            base.Channel.CheckOffers();
        }
        
        public System.Threading.Tasks.Task CheckOffersAsync() {
            return base.Channel.CheckOffersAsync();
        }
    }
}