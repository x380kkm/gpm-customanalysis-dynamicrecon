using Cameca.CustomAnalysis.Interface;
using Prism.Commands;
using Prism.Events;

namespace GPM.CustomAnalysis.DynamicReconV2;

internal class DynamicReconV2ParamsNodeMenuFactory : IAnalysisMenuFactory
{

	public const string UniqueId = "GPM.CustomAnalysis.DynamicReconV2.DynamicReconV2ParamsNodeMenuFactory";

	private readonly IEventAggregator _eventAggregator;

	public DynamicReconV2ParamsNodeMenuFactory(IEventAggregator eventAggregator)
	{
		_eventAggregator = eventAggregator;
	}

	public IMenuItem CreateMenuItem(IAnalysisMenuContext context) => new MenuAction(
		DynamicReconV2ParamsNode.DisplayInfo.Title,
		new DelegateCommand(() => _eventAggregator.PublishCreateNode(
			DynamicReconV2ParamsNode.UniqueId,
			context.NodeId,
			DynamicReconV2ParamsNode.DisplayInfo.Title,
			DynamicReconV2ParamsNode.DisplayInfo.Icon)),
		DynamicReconV2ParamsNode.DisplayInfo.Icon);

	public AnalysisMenuLocation Location { get; } = AnalysisMenuLocation.Analysis;
}
