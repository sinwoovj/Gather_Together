using System.Collections;
using System.Collections.Generic;

public class MainQuestData
{
    public string MainQuestName; //quest 이름
    public long[] npcId; //그 퀘스트와 연관되어 있는 npc의 Id

    //구조체 생성을 위해 매개변수 생성자를 작성
    public MainQuestData(string name, long[] npc)
    {
        MainQuestName = name;
        npcId = npc;
    }
}
