using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;
using Cameca.CustomAnalysis.Interface;
using Cameca.CustomAnalysis.Utilities;

namespace GPM.CustomAnalysis.DynamicReconV2;

[DefaultView(DynamicReconV2ParamsViewModel.UniqueId, typeof(DynamicReconV2ParamsViewModel))]
internal class DynamicReconV2ParamsNode : AnalysisNodeBase
{
	public class NodeDisplayInfo : INodeDisplayInfo
	{
		public string Title { get; } = "GPM Dynamic Recon Params";
		public ImageSource? Icon { get; } = null;
	}

	public static NodeDisplayInfo DisplayInfo { get; } = new();

	public const string UniqueId = "GPM.CustomAnalysis.DynamicRecon.DynamicReconParamsNode";

	private readonly DynamicReconV2Params dynamicReconV2Param;

	public DynamicReconV2ParamsOptions Options { get; private set; } = new();

	public DynamicReconV2ParamsNode(IAnalysisNodeBaseServices services) : base(services)
	{
		dynamicReconV2Param = new DynamicReconV2Params();
	}

	public async Task<DynamicReconV2Results?> Run()
	{
		if (await Services.IonDataProvider.GetIonData(InstanceId) is not { } ionData)
			return null;

		return dynamicReconV2Param.Run(ionData, Options);
	}

	protected override byte[]? GetSaveContent()
	{
		var serializer = new XmlSerializer(typeof(DynamicReconV2ParamsOptions));
		using var stringWriter = new StringWriter();
		serializer.Serialize(stringWriter, Options);
		return Encoding.UTF8.GetBytes(stringWriter.ToString());
	}

	protected override void OnCreated(NodeCreatedEventArgs eventArgs)
	{
		base.OnCreated(eventArgs);
		Options.PropertyChanged += OptionsOnPropertyChanged;

        if (eventArgs.Trigger == EventTrigger.Load && eventArgs.Data is { } data)
        {
            var xmlData = Encoding.UTF8.GetString(data);
            var serializer = new XmlSerializer(typeof(DynamicReconV2ParamsOptions));
            using var stringReader = new StringReader(xmlData);
            if (serializer.Deserialize(stringReader) is DynamicReconV2ParamsOptions loadedOptions)
            {
                Options = loadedOptions;
            }
        }
	}

	private void OptionsOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (CanSaveState is { } canSaveState)
		{
			canSaveState.CanSave = true;
		}
	}
}
