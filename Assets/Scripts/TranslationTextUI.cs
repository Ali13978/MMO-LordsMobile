using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class TranslationTextUI : MonoBehaviour
{
	public string textPrevious = string.Empty;

	public string textLabel;

	public string textPost = string.Empty;

	private Text myText;

	private void Awake()
	{
		myText = base.gameObject.GetComponent<Text>();
		myText.text = textPrevious + ScriptLocalization.Get(textLabel).ToUpper() + textPost;
	}
}
