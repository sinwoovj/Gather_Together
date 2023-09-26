using DI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class ChatManager : DIMono
{
    public Texture2D Portait;
    private string Portait_Address;

    [Inject]
    GameData gameData;
    public GameObject TalkPanel;

    public void Chat(IEnumerable<SceneLine> sceneLines, SceneLine line)
    {
        // 채팅 화면에보여주기
        TalkPanel.GetComponent<Animator>().SetBool("isShow", true);
        // 초상화 설정
        Transform TalkPanel_Portrait = TalkPanel.transform.Find("Portrait");


        Portait_Address = gameData.HostImage.Single(
           l => l.hostCode == line.hostCode
        && l.clothCode == line.clothCode
        && l.hostEmotion == line.hostEmotion).assetPath;
     

        var portait = Addressables.LoadAssetAsync<Texture2D>(Portait_Address).WaitForCompletion();
        var rawImage = TalkPanel_Portrait.GetComponent<RawImage>();


        rawImage.texture = portait;
        // 이름 설정
        Transform TalkPanel_Name = TalkPanel.transform.Find("Name");
        Member interlocutor = gameData.Member.FirstOrDefault(l => l.Id == line.hostCode);

        if (interlocutor != null)
        {
            TalkPanel_Name.GetComponent<Text>().text = interlocutor.Name;
        }
        // 대화 설정
        Transform TalkPanel_Script = TalkPanel.transform.Find("Script");


        TalkPanel_Script.GetComponent<Text>().text = line.content;
    }

    internal void CloseChat()
    {
        TalkPanel.GetComponent<Animator>().SetBool("isShow", false);
    }
}
