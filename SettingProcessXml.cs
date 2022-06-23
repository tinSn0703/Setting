using System.Xml.Linq;

namespace Setting
{
	/// <summary>XMLの設定ファイルから設定を読み出す方法を表す</summary>
	public abstract class SettingProcessXml : SettingProcess
	{
		public SettingProcessXml() {}

		public abstract void WriteElement(in XElement _Element);
		public abstract XElement ReadElement();
	}
}
