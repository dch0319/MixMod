using System.ComponentModel;

namespace MixMod
{
	public enum CardState
	{
		[Description("默认")]
		Default,
		[Description("仅己方实体")]
		OnlyMy,
		[Description("全部实体")]
		All,
		[Description("禁用")]
		Disabled
	}
}
