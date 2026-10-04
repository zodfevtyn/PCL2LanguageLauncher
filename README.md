# PCL2 Language Launcher

一個開源的 Windows 第三方輔助工具，用於在特定 Windows 系統語言環境下啟動 Plain Craft Launcher 2（PCL2）。

本專案的目的非常單純：

在不修改 PCL2 本體的情況下，暫時將 Windows 使用者介面語言覆寫為 zh-CN，啟動 PCL2，並在 PCL2 關閉後自動恢復原本的 Windows UI Language Override。
（該專案主要面嚮與處於中國大陸地區，設備語言非簡體中文，且未使用正版登入的用戶）

本專案完全開源，不包含 PCL2，也不包含 Minecraft。

⸻

## 專案定位

PCL2 Language Launcher 是一個獨立的第三方 Windows 輔助工具。

本專案不是 PCL2 的修改版，也不是 PCL2 的重新實作。

本工具不：

* 修改 PCL2 程式檔案
* 修改 PCL2 DLL
* 修改 PCL2 資源
* 向 PCL2 注入 DLL
* 使用 API Hook 修改 PCL2 行為
* 破解 PCL2
* 破解 Minecraft
* 提供 Minecraft 帳戶
* 提供 Minecraft 遊戲檔案
* 重新分發 PCL2

本工具只利用 Windows 本身提供的 UI Language Override 機制，在啟動 PCL2 前暫時調整使用者介面語言環境。

⸻

## 為什麼需要這個工具？

在部分 Windows 語言環境下，PCL2 可能存在與系統使用者介面語言相關的啟動限制或環境要求。

例如，使用：

English (United Kingdom)
English (United States)
其他非簡體中文 Windows 顯示語言

的使用者，在某些情況下可能無法正常啟動 PCL2。

另一方面，一些使用者並不希望為了啟動 PCL2，而永久將整個 Windows 的顯示語言切換成：

中文（簡體，中國）
zh-CN

因此，本工具提供一個臨時的解決方案。

⸻

## 工作原理

本工具不修改 PCL2 本身。

啟動流程如下：

讀取目前 Windows UI Language Override
                │
                ▼
暫時設定為 zh-CN
                │
                ▼
啟動 PCL2
                │
                ▼
等待 PCL2 關閉
                │
                ▼
恢復原本的 UI Language Override

主要使用 Windows PowerShell 提供的語言設定命令：

Get-WinUILanguageOverride
Set-WinUILanguageOverride

其中：

Set-WinUILanguageOverride -Language zh-CN

用於啟動 PCL2 前暫時設定 zh-CN。

PCL2 結束後，本工具會恢復啟動前記錄的設定。

如果啟動前沒有 UI Language Override，則會恢復為沒有 Override 的狀態。

⸻

## 主要功能

* 開源
* Windows 10 / Windows 11
* .NET Framework 4.8
* 自動尋找 PCL2
* 找不到 PCL2 時可以手動選擇
* 自動記住 PCL2 路徑
* 啟動 PCL2 前暫時設定 zh-CN
* PCL2 關閉後自動恢復原本的 UI Language Override
* 不修改 PCL2
* 不注入 PCL2
* 不使用 API Hook
* 不包含 PCL2
* 不包含 Minecraft

⸻

## 適用使用者

本專案主要面向：

**身處中國大陸、尚未使用 Minecraft 正版帳戶，並且 Windows 使用者介面語言不是簡體中文 zh-CN 的使用者。**

這是本專案最主要的使用場景。

如果你的 Windows 本身就是簡體中文 zh-CN，通常沒有使用本工具的必要。

如果你已經購買 Minecraft 正版，也可以使用本工具；本工具與 Minecraft 帳戶是否為正版沒有技術上的綁定。

但是，我們始終支持 Minecraft 正版購買與合法使用。

⸻

## 支持 Minecraft 正版

如果你有能力，請購買正版 Minecraft

本專案明確支持 Minecraft 正版購買與合法使用。

如果你有能力購買正版 Minecraft，我們強烈建議：

購買正版、使用正版帳戶，並通過官方方式使用 Minecraft。

本工具不是為了鼓勵盜版 Minecraft。

本工具也不是用於破解 Minecraft 正版授權。

本工具不提供：

* Minecraft 破解
* Minecraft 盜版帳戶
* Minecraft 遊戲檔案
* Microsoft 帳戶
* Minecraft 授權繞過
* Minecraft 登入繞過

**如果你目前沒有購買正版的條件，本專案只提供 Windows 層面的 PCL2 啟動輔助。**

當你未來具備購買條件時，我們仍然建議你購買正版。

如果你喜歡 Minecraft，請支持 Minecraft 的開發者。

⸻

## Minecraft 官方資源

如果你尚未購買 Minecraft 正版，可以通過 Minecraft 官方網站了解及購買。

**Minecraft 正版購買**

前往 Minecraft 官方網站購買 Minecraft Java 版與 Bedrock 版

Minecraft 官方目前提供 PC 版 Minecraft Java Edition 與 Bedrock Edition。官方網站也提供 Minecraft Launcher 等相關下載。(Minecraft.net)

**Minecraft 官方下載**

前往 Minecraft 官方下載頁面

請優先從 Minecraft 官方網站取得 Minecraft Launcher 及其他官方軟體。(Minecraft.net)

⸻

## PCL2

PCL2（Plain Craft Launcher 2） 是一個獨立的 Minecraft 啟動器。

PCL 社區目前公開了 PCL 的大部分源代碼，供社區研究及相關開發使用。官方公開倉庫目前包含 PCL 的 UI 庫、動畫模組、下載模組、Minecraft 啟動模組等大部分源代碼。(GitHub)

PCL2 官方 GitHub

Plain Craft Launcher — PCL2-4941

請注意：

本專案不包含 PCL2。

使用者需要自行取得 PCL2。

⸻

## PCL 相關授權與合理使用

PCL 官方公開了《PCL 分發有限許可》以及《PCL 存儲庫合理使用指南》。

PCL 的公開指南明確要求第三方項目不要與 PCL 官方項目產生混淆，也不要暗示第三方項目與 PCL 官方存在關係。

對於涉及 PCL 實質功能的第三方項目，指南還規定了更嚴格的要求；其中包括公開源代碼、第三方身份說明，以及涉及 Minecraft 啟動功能時的正版購買勸導等要求。(GitHub)

本專案沒有基於 PCL 源代碼修改 PCL，也沒有重新實現 Minecraft 啟動功能。

因此，本專案將自身定位為：

獨立的第三方 Windows 輔助工具。

PCL 相關授權文件

PCL 分發有限許可與合理使用指南

在使用、修改或重新分發 PCL 相關內容時，請自行閱讀並遵守相應的授權與使用要求。

⸻

## 本專案與 PCL2 的關係

本專案：

* 不屬於 PCL 官方開發團隊
* 不代表 PCL 官方
* 不由 PCL 官方維護
* 不受 PCL 官方委託
* 不與 PCL 官方建立合作關係
* 不修改 PCL2
* 不重新分發 PCL2
* 不包含 PCL2
* 不包含 Minecraft

「PCL2 Language Launcher」中的 PCL2 仅用于说明本工具的用途。

本專案不聲稱與 PCL 官方存在任何隸屬、合作、授權或贊助關係。

⸻

## 使用方式

1. 取得本工具

從 GitHub Releases 下載：

PCL2LanguageLauncher.exe

或者自行從源代碼建置。

2. 啟動

直接執行：

PCL2LanguageLauncher.exe

程式會首先嘗試自動尋找：

Plain Craft Launcher 2.exe

3. 首次使用

如果無法自動找到 PCL2，程式會要求你手動選擇：

Plain Craft Launcher 2.exe

選擇完成後，程式會保存 PCL2 的路徑。

4. 再次使用

之後啟動本工具時，會優先使用之前保存的 PCL2 路徑。

⸻

## 使用流程

例如你的 Windows 顯示語言為：

English (United Kingdom)

啟動本工具後：

Windows UI Language Override
        │
        ├── 原本：無 Override
        │
        ▼
暫時設定：zh-CN
        │
        ▼
啟動 PCL2
        │
        ▼
使用 PCL2
        │
        ▼
關閉 PCL2
        │
        ▼
恢復：無 Override

因此，你不需要永久將 Windows 顯示語言修改成簡體中文。

⸻

## 隱私與安全

本工具本身不需要登入任何帳戶。

本工具不需要：

* Microsoft 帳戶
* Minecraft 帳戶
* PCL 帳戶
* 遠端服務
* 第三方 API

本工具主要執行以下操作：

1. 讀取 Windows UI Language Override。
2. 保存原本設定。
3. 暫時設定 zh-CN。
4. 啟動使用者指定的 PCL2。
5. 等待 PCL2 結束。
6. 恢復原本的 UI Language Override。

PCL2 的帳戶登入、Minecraft 登入以及遊戲本身的網路通信由 PCL2 / Minecraft 自身負責。

⸻

## 開源

本專案完全開源。

你可以查看：

* 完整源代碼
* Git Commit
* 建置設定
* Issue
* Pull Request
* Release

本專案希望通過開源方式讓使用者清楚了解程式的工作方式。

任何人都可以檢查本工具對 Windows 所執行的操作。

如果你發現任何安全問題、功能問題或其他值得改進的地方，歡迎提交 Issue 或 Pull Request。

⸻
## 建置

開發環境

本專案目前使用：

Visual Studio 2026
.NET Framework 4.8
C#

Solution 使用 Visual Studio 新版：

.slnx

專案可以直接使用 Visual Studio 開啟：

PCL2LanguageLauncher.slnx

然後選擇：

Release
x64

進行建置。

⸻

專案結構

PCL2LanguageLauncher/
│
├── PCL2LanguageLauncher/
│   ├── Program.cs
│   ├── PCL2LanguageLauncher.csproj
│   └── ...
│
├── .gitignore
├── LICENSE
├── README.md
└── PCL2LanguageLauncher.slnx

PCL2 本體不包含在本專案中。

⸻

## 問題回報

如果你遇到本工具自身的問題，例如：

* 無法找到 PCL2
* 無法啟動 PCL2
* Windows 語言設定沒有恢復
* 程式發生錯誤
* 某個 Windows 版本無法正常使用
* 程式存在安全問題

歡迎在 GitHub Issues 中回報。

提交 Issue 時，建議提供：

* Windows 版本
* PCL2 版本
* 本工具版本
* 錯誤訊息
* 重現步驟

請不要在 Issue 中公開：

* Microsoft 帳戶密碼
* Minecraft 帳戶密碼
* Access Token
* Session
* Cookie
* 其他敏感資訊

⸻

## 合法性、授權及撤銷聯絡

**本專案作者希望以善意、透明、開源及尊重第三方權利的方式提供本工具。**

如果任何：

* PCL2 開發者
* PCL 相關項目維護者
* Minecraft 相關權利人
* 其他著作權或商標權利人
* 法律或合規相關人士

認為本專案存在：

* 著作權問題
* 授權問題
* 商標問題
* 名稱使用問題
* 法律合規問題
* 與 PCL 相關規範不一致
* 其他不適當內容

或者相關開發者、權利人希望本專案：

* 修改相關內容
* 移除相關內容
* 停止發布
* 下架 Release
* 撤銷本專案
* 停止維護

**請直接聯絡作者：**

zodfevtyn21@gmail.com

我們會認真查看相關要求，並在合理範圍內進行處理。

**如果你是 PCL2 相關開發者或權利人，也歡迎直接透過上述電子郵件聯絡，而不需要先通過其他渠道。**

⸻

## 免責聲明

本工具按照「現狀」提供。

作者不保證本工具：

* 永久兼容所有 Windows 版本
* 永久兼容所有 PCL2 版本
* 能夠解決 PCL2 的所有啟動問題
* 能夠解決 Minecraft 的登入問題
* 能夠解決 Minecraft 的授權問題

Windows 或 PCL2 未來更新後，本工具可能需要相應修改。

使用者應自行判斷是否使用本工具。

⸻

## 第三方商標與智慧財產權

Minecraft、Minecraft Java Edition、Minecraft Bedrock Edition、Microsoft 以及相關名稱、標誌與智慧財產權屬於其各自權利人。

Plain Craft Launcher、PCL、PCL2 以及相關名稱、程式碼、資源與智慧財產權屬於其相應權利人。

本專案不主張擁有上述第三方資產的任何權利。

本專案也不授予任何人使用、修改或重新分發第三方軟體的權利。

第三方軟體仍然受到其自身授權條款約束。

⸻

## License

本專案本身採用 MIT License。

請參閱：

LICENSE

MIT License 僅適用於本專案自身的原始碼。

它不適用於：

* Minecraft
* PCL2
* PCL 相關源代碼
* PCL2 的二進制文件
* 其他第三方軟體
* 第三方商標
* 第三方資源

使用第三方軟體時，請遵守其各自的授權條款。

⸻

## 官方資源

資源	連結
Minecraft 官方購買	https://www.minecraft.net/zh-hans/store/minecraft-java-bedrock-edition-pc
Minecraft 官方下載	https://www.minecraft.net/download/
PCL2 GitHub	https://github.com/PCL-Community/PCL2-4941
PCL 授權與合理使用指南	https://github.com/PCL-Community/PCL2-Language/blob/main/LICENCE
本專案	https://github.com/你的帳號/PCL2LanguageLauncher

⸻

最後

本專案只是希望解決一個非常具體的 Windows 使用環境問題：

讓使用非簡體中文 Windows 的中國大陸使用者，在不永久修改 Windows 顯示語言的情況下啟動 PCL2。

我們尊重 PCL 開發者、Minecraft 開發者以及所有相關權利人的工作。

我們支持 PCL 的正常使用，也支持 Minecraft 正版。

如果你有能力購買正版 Minecraft，請購買正版。

同時，如果你認為本專案存在任何問題，無論是技術問題、授權問題、法律問題還是開發者希望撤銷本專案，都歡迎聯絡：

zodfevtyn21@gmail.com

感謝所有開源軟體與 Minecraft 社區的開發者。
