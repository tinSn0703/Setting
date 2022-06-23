
namespace Setting
{
	/// <summary>設定ファイルから読み出す設定の基底クラス</summary>
	public abstract class Setting
	{
		public Setting() {}

		/// <summary>設定データにデフォルトの設定を入力する</summary>
		public abstract void SetDafultSetting();

		/// <summaryフォーム上の設定の要素名</summary>
		public abstract string SettingName { get; }
	}
}
