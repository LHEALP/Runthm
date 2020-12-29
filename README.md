# Runthm (Unity RhythmGame Editor)
2020-12-29 23:24 (UTC + 9) Modified<br/><br/>
<img src="https://healp-32446.web.app/img/urge_preview.gif" width="718px" height="489px" alt="preview"></img><br/><br/><br/>

## 가이드 Guide
이 프로젝트는 유니티 2019.4.9f1 에서 생성되었습니다.<br/><br/>
<img src="https://healp-32446.web.app/img/urge_guide.png" width="80%" alt="guide"></img><br/>

1. Resources 폴더에 있는 폴더 이름을 입력해주세요.
2. Load 버튼을 눌러 해당 시트를 불러올 수 있습니다. Save 버튼을 눌러 현재 상태를 저장하거나, 1번에서 새로운 폴더 이름을 입력했다면 해당 이름을 가진 폴더가 생성됩니다.
3. 오프셋 설정 버튼입니다. **필요 시** 사용할 수 있습니다.
4. 음악 컨트롤 바입니다.

<img src="https://healp-32446.web.app/img/urge_guide2.png" width="70%" alt="guide2"></img><br/>

Milky Way 를 입력하고 Load 버튼을 누른 모습.

시트 제작을 끝마쳤다면 해당 폴더를 https://github.com/LHEALP/UnityRhythmGame 의 Resources 폴더로 옮겨주면, 얼마 전 리스트 자동생성을 구현함에 따라 알아서 화면에 등장할 것입니다.<br/>
이 과정이 번거롭다면, 현재 프로젝트의 FileIO.cs 에서 현 프로젝트의 Resources 폴더가 아닌 UnityRhythmGame 프로젝트의 Resources 폴더로 저장/로드하게끔 경로를 바꿔주시면 됩니다.

## 핫키 Hotkey
스페이스 - 재생/일시정지 ( Space - Play/Puase )<br/>
마우스좌클릭 - 노트 배치 ( Mouse leftBtn - Dispose note )<br/>
마우스우클릭 - 노트 삭제 ( Mouse rightBtn - Cancel note )<br/>
마우스휠 - 음악 및 그리드 위치 이동 ( Mouse wheel - Move music and grids pos )<br/>
컨트롤 + 마우스휠 - 그리드 스냅 변경 ( Ctrl + Mouse wheel - Change snap of grids )<br/>

## 주저리 blahblahhhhhh
버그가 굉장히 많을 것으로 예상됩니다. 충분한 테스트를 진행하지 못했기 때문입니다.<br/>
하지만 에디터를 어떻게 설계하고 구현했는지 보실 수 있습니다.<br/>
노트 에디터 구현 관련해서 구글링을 해본 적은 없지만, 리듬게임 자체도 자료가 한정적이었던 것을 감안하면 에디터는 더욱 그럴지도 모르겠습니다.
프로젝트 가이드가 부족하지만.. 언젠가 기회(사실 게으름과 시간여유와의 싸움)가 오면 조금 더 상세하게 적어놓겠습니다.<br/>
또 다시 누군가에게 이 보잘것 없는 프로젝트가 도움이 되길 바랍니다.<br/>
<br/>
LHEALP
