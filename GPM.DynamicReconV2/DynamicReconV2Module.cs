using Cameca.CustomAnalysis.Interface;
using Cameca.CustomAnalysis.Utilities;
using Prism.Ioc;
using Prism.Modularity;

namespace GPM.CustomAnalysis.DynamicReconV2;

public class DynamicReconV2Module : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.AddCustomAnalysisUtilities();

        containerRegistry.Register<object, DynamicReconV2ParamsNode>(DynamicReconV2ParamsNode.UniqueId);
        containerRegistry.RegisterInstance<INodeDisplayInfo>(DynamicReconV2ParamsNode.DisplayInfo, DynamicReconV2ParamsNode.UniqueId);
        containerRegistry.Register<IAnalysisMenuFactory, DynamicReconV2ParamsNodeMenuFactory>(DynamicReconV2ParamsNodeMenuFactory.UniqueId);
        containerRegistry.Register<object, DynamicReconV2ParamsViewModel>(DynamicReconV2ParamsViewModel.UniqueId);
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        var extensionRegistry = containerProvider.Resolve<IExtensionRegistry>();
        extensionRegistry.RegisterAnalysisView<DynamicReconV2ParamsView, DynamicReconV2ParamsViewModel>(AnalysisViewLocation.Top);
    }
}