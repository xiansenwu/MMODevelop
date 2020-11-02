local Directory = CS.System.IO.Directory;
local File = CS.System.IO.File;

local function RequireMod(modPath)
	local luaFilePath = App.project.basePath..modPath
	local file = io.open(luaFilePath,"r")
	local luaFileStr = file:read("*a")
	file:close()
	local luaFnc = load(luaFileStr)
	
	--fprint("luaFnc = "..tostring(luaFnc))
	return luaFnc()
end

Window = {}
local TypeParse = RequireMod('/plugins/AutoCode/TypeParse.lua')

Window.CostomFuntionStart = '//--------******Alphas*CostomFuntion******--------//start'
Window.CostomFuntionStr = ''
Window.CostomFuntionEnd   = '//--------******Alphas*CostomFuntion******--------//end'
Window.NameSpaceStart = '//--------******Alphas*NameSpace******--------//start'
Window.NameSpaceStr = ''
Window.NameSpaceEnd   = '//--------******Alphas*NameSpace******--------//end'
Window.OnInitStart = '//--------******Alphas*OnInit******--------//start'
Window.OnInitStr = ''
Window.OnInitEnd   = '//--------******Alphas*OnInit******--------//end'
Window.OnBtnClickBindStart = '//--------******Alphas*OnBtnClickBind******--------//start'
Window.OnBtnClickBindStr = ''
Window.OnBtnClickBindEnd   = '//--------******Alphas*OnBtnClickBind******--------//end'

Window.Resources = {'Start_Window'}
Window.isResource = false
function Window.reset()
	Window.isResource = false
	Window.FuntionStr = ''
	Window.NameSpaceStr = ''
	Window.OnInitStr = ''
	Window.OnBtnClickBindStr = ''
end
function Window.paiseWindowClass(handler,classInfo,_ExportSetting)
		Window.reset()
		local getMemberByName = _ExportSetting.settings.getMemberByName
		local strIndex =  string.find(classInfo.className,'_Window')
		fprint("getMemberByName = "..tostring(getMemberByName))
		if strIndex ~= nil then 
			local writer = CodeWriter.new()
			local wndName = string.sub(classInfo.className,1,strIndex-1)
			wndName = wndName.."Window"
			local wndViewName = wndName.."View"
			Window.wndName = wndName
			Window.wndViewName = wndViewName
			fprint("wndName = "..tostring(wndName))
			Window.isResource = TypeParse.IsInTable(classInfo.className,Window.Resources)

			local members = classInfo.members
			writer:reset()

			writer:writeln('using FairyGUI;')
			writer:writeln('using FairyGUI.Utils;')
			writer:writeln()
			writer:writeln('namespace %s', _ExportSetting.namespaceName)
			writer:startBlock()
			writer:writeln('public partial class %s : %s', wndViewName, classInfo.superClassName)
			writer:startBlock()
			--成员变量
			local memberCnt = members.Count
			for j=0,memberCnt-1 do
				local memberInfo = members[j]
				local memberType = TypeParse.ParseMembers(memberInfo)
				fprint("ParseClass memberType = "..tostring(memberInfo.type).." memberInfo.varName = "..memberInfo.varName)
				writer:writeln('public %s %s;', memberType, memberInfo.varName)
			end
			writer:writeln('public static string PackageName = "%s";', handler.pkg.name)
			writer:writeln('public static string ResName = "%s";', classInfo.resName)
			writer:writeln('public const string URL = "ui://%s%s";', handler.pkg.id, classInfo.resId)
			writer:writeln()

			writer:writeln('public static %s CreateInstance()', wndViewName)
			writer:startBlock()
			writer:writeln('return (%s)UIPackage.CreateObject(PackageName, ResName);', wndViewName)
			writer:endBlock()
			writer:writeln()

			if handler.project.type==ProjectType.MonoGame then
				writer:writeln("protected override void OnConstruct()")
				writer:startBlock()
			else
				writer:writeln('public override void ConstructFromXML(XML xml)')
				writer:startBlock()
				writer:writeln('base.ConstructFromXML(xml);')
				writer:writeln('this.fairyBatching = true;')
				writer:writeln()
			end
			for j=0,memberCnt-1 do
				local memberInfo = members[j]
				if memberInfo.group==0 then
					local memberType = TypeParse.ParseMembers(memberInfo)
					if getMemberByName then
						writer:writeln('%s = (%s)GetChild("%s");', memberInfo.varName, memberType, memberInfo.name)
					else
						writer:writeln('%s = (%s)GetChildAt(%s);', memberInfo.varName, memberType, memberInfo.index)
					end
				elseif memberInfo.group==1 then
					if getMemberByName then
						writer:writeln('%s = GetController("%s");', memberInfo.varName, memberInfo.name)
					else
						writer:writeln('%s = GetControllerAt(%s);', memberInfo.varName, memberInfo.index)
					end
				else
					if getMemberByName then
						writer:writeln('%s = GetTransition("%s");', memberInfo.varName, memberInfo.name)
					else
						writer:writeln('%s = GetTransitionAt(%s);', memberInfo.varName, memberInfo.index)
					end
				end
			end
			writer:endBlock()

			writer:endBlock() --class
			writer:endBlock() --namepsace
			writer:save(_ExportSetting.exportCodeViewPath..'/'..Window.wndViewName..'.cs')
			
			--window逻辑类
			Window.getCostomStr(classInfo,_ExportSetting)
			Window.ceateNewFile(writer,classInfo,_ExportSetting)
			Window.ceateNewFileLogic(writer,classInfo,_ExportSetting)
			
			
		end
		return Window
end
function Window.getCostomStr(classInfo,_ExportSetting)
			local wndPath = _ExportSetting.exportCodeViewPath..'/'..Window.wndName..'.cs'
			local wndLogicPath = _ExportSetting.exportCodePath..'/'..Window.wndName..'Logic.cs'
			if File.Exists(wndPath) == true then
				local luaFileStr = Window.GetFileAllText(wndPath)
				Window.NameSpaceStr = TypeParse.getCostomStr(luaFileStr,Window.NameSpaceStart,Window.NameSpaceEnd)
				Window.OnInitStr = TypeParse.getCostomStr(luaFileStr,Window.OnInitStart,Window.OnInitEnd)
				Window.CostomFuntionStr = TypeParse.getCostomStr(luaFileStr,Window.CostomFuntionStart,Window.CostomFuntionEnd)
				
			end
			if File.Exists(wndLogicPath) == true then
				local luaFileStr = Window.GetFileAllText(wndLogicPath)
				Window.OnBtnClickBindStr = TypeParse.getCostomStr(luaFileStr,Window.OnBtnClickBindStart,Window.OnBtnClickBindEnd)
			end
end
function Window.GetFileAllText(path)
	if File.Exists(path) == true then
		local file = io.open(path,"r")
		local luaFileStr = file:read("*a")
		file:close()
		return luaFileStr
	end
	return ''
end
function Window.WriterOnBtnClickBind(writer,classInfo)
			memberCnt = classInfo.members.Count
			for j=0,memberCnt-1 do
				local memberInfo = classInfo.members[j]
				local memberType = TypeParse.ParseMembers(memberInfo)
				local funName = ''
				local funNameIndex = nil
				if memberType == 'GButton' then
					funName = 'private void OnClick'..memberInfo.name..'(EventContext context)'
					funNameIndex =  string.find(Window.OnBtnClickBindStr,funName,1,true)
				elseif memberType == 'GComboBox' then
					funName = 'private void OnChanged'..memberInfo.name..'(EventContext context)'
					funNameIndex =  string.find(Window.OnBtnClickBindStr,funName,1,true)
				end
				if funNameIndex == nil and funName ~='' then
					writer:writeln(funName)
					writer:startBlock()
					writer:writeln()
					writer:endBlock()
				end
			end
end
function Window.ceateNewFileLogic(writer,classInfo,_ExportSetting)
		local wndLogicPath = _ExportSetting.exportCodePath..'/'..Window.wndName..'Logic.cs'
		writer:reset()
		if File.Exists(wndLogicPath) == false then
			writer:writeln('using ET;')
			writer:writeln('using FairyGUI;')
			writer:writeln('namespace %s', _ExportSetting.namespaceName)
			writer:startBlock()--namespaceName start
			writer:writeln('public partial class %s : %s', Window.wndName, 'UIWindowForm')
			writer:startBlock()--class start
			writer:writeln('protected void OnInit_Supplement()')
			writer:startBlock()
			writer:endBlock()
			writer:writeln('protected void OnBindEvent_Supplement()')
			writer:startBlock()
			writer:endBlock()
			--OnBtnClickBind
			table.insert(writer.lines, Window.OnBtnClickBindStart)
			table.insert(writer.lines, Window.OnBtnClickBindStr)
			Window.WriterOnBtnClickBind(writer,classInfo)
			
			table.insert(writer.lines, Window.OnBtnClickBindEnd)
			writer:endBlock()--class end
			writer:endBlock()--namespaceName end
			writer:save(wndLogicPath)
		else
			writer.lines = {}
			local luaFileStr = Window.GetFileAllText(wndLogicPath)
			--luaFileStr
			local str ,beforestr,endstr = TypeParse.getCostomStr(luaFileStr,Window.OnBtnClickBindStart,Window.OnBtnClickBindEnd,Window.wndName)
			--fprint("str = "..tostring(str))
			--fprint("beforestr = "..tostring(beforestr))
			--fprint("endstr = "..tostring(endstr))

			table.insert(writer.lines, beforestr)
			table.insert(writer.lines, str)

			Window.WriterOnBtnClickBind(writer,classInfo)

			table.insert(writer.lines, endstr)
			writer:save(wndLogicPath)
		end
end

function Window.ceateNewFile(writer,classInfo,_ExportSetting)
			writer:reset()
			writer:writeln('using ET;')
			writer:writeln('using FairyGUI;')
			table.insert(writer.lines, Window.NameSpaceStart)
			table.insert(writer.lines, Window.NameSpaceStr)
			table.insert(writer.lines, Window.NameSpaceEnd)
			writer:writeln()
			writer:writeln('namespace %s', _ExportSetting.namespaceName)
			--namespaceName start
			writer:startBlock()
			writer:writeln('public partial class %s : %s', Window.wndName, 'UIWindowForm')
			--class start
			writer:startBlock()
			writer:writeln('public %s view;',Window.wndViewName)
			--构造函数
			if Window.isResource == true then
				writer:writeln('public %s() : base(%s.PackageName, %s.ResName,true)',Window.wndName, Window.wndViewName, Window.wndViewName)
			else
				writer:writeln('public %s() : base(%s.PackageName, %s.ResName)',Window.wndName, Window.wndViewName, Window.wndViewName)
			end
			writer:startBlock()
			writer:writeln('BindItemExtension();')
			writer:endBlock()
			--onInit 初始化
			writer:writeln('protected override void OnInit()')
			writer:startBlock()
			writer:writeln('base.OnInit();')
			writer:writeln('view = GuiComponent as %s;',Window.wndViewName)
			--writer:writeln('BindCallbacks();')
			table.insert(writer.lines, Window.OnInitStart)
			table.insert(writer.lines, Window.OnInitStr)
			table.insert(writer.lines, Window.OnInitEnd)
			writer:writeln('OnInit_Supplement();')
			writer:endBlock()
			--BindItemExtension 绑定拓展类
			writer:writeln('public static void BindItemExtension()')
			writer:startBlock()
			writer:writeln('UIObjectFactory.SetPackageItemExtension(%s.URL, typeof(%s));',Window.wndViewName,Window.wndViewName)
			writer:writeln('//调用子节点BindItemExtension()')
			--成员变量
			
			local memberCnt = classInfo.members.Count
			for j=0,memberCnt-1 do
				local memberInfo = classInfo.members[j]
				local memberType = TypeParse.ParseMembers(memberInfo)
				local isGeneralClass = TypeParse.IsInTable(memberType,_ExportSetting.GeneralClass)
				if isGeneralClass == false then 
					writer:writeln('UIObjectFactory.SetPackageItemExtension(%s.URL, typeof(%s));',memberType,memberType)
				end
			end
			writer:endBlock()
			--OnBindEvent 绑定事件
			writer:writeln('protected override void OnBindEvent()')
			writer:startBlock()
			local hasWindowTitle = false
			local memberCnt = classInfo.members.Count
			for j=0,memberCnt-1 do
				local memberInfo = classInfo.members[j]
				local memberType = TypeParse.ParseMembers(memberInfo)
				if memberType == 'GButton' then
					writer:writeln('view.%s%s.onClick.Add(OnClick%s);',_ExportSetting.settings.memberNamePrefix,memberInfo.name,memberInfo.name)
				elseif memberType == 'GComboBox' then
					writer:writeln('view.%s%s.onChanged.Add(OnChanged%s);',_ExportSetting.settings.memberNamePrefix,memberInfo.name,memberInfo.name)
				elseif memberType == 'WindowTitle' then
					writer:writeln('view.%s%s.m_closeBtn.onClick.Add(OnClickCloseBtn);',_ExportSetting.settings.memberNamePrefix,memberInfo.name)
					writer:writeln('view.%s%s.m_homeBtn.onClick.Add(OnClickHomeBtn);',_ExportSetting.settings.memberNamePrefix,memberInfo.name)
					hasWindowTitle = true
				end
				--local memClasses = handler:CollectClasses(settings.ignoreNoname, settings.ignoreNoname, fguiNamespace)
			end
			writer:writeln('OnBindEvent_Supplement();')
			writer:endBlock()
			if hasWindowTitle == true then
				writer:writeln('private void OnClickCloseBtn(EventContext context)')
				writer:startBlock()
				writer:writeln('UIComponent.Instance.HideWindow(this);')
				writer:endBlock()
				writer:writeln('private void OnClickHomeBtn(EventContext context)')
				writer:startBlock()
				writer:writeln('UIComponent.Instance.HideAllWindows();')
				writer:endBlock()
			end
			--不可更改区域
			table.insert(writer.lines, Window.CostomFuntionStart)
			table.insert(writer.lines, Window.CostomFuntionStr)
			table.insert(writer.lines, Window.CostomFuntionEnd)
			--class end
			writer:endBlock()
			--namespaceName end
			writer:endBlock()
			writer:save(_ExportSetting.exportCodeViewPath..'/'..Window.wndName..'.cs')
end



return Window