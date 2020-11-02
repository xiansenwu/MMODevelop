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

CostomClass = {}
local TypeParse = RequireMod('/plugins/AutoCode/TypeParse.lua')

CostomClass.CostomFuntionStart = '//--------******Alphas*CostomFuntion******--------//start'
CostomClass.CostomFuntionStr = ''
CostomClass.CostomFuntionEnd   = '//--------******Alphas*CostomFuntion******--------//end'
CostomClass.OnInitStart = '//--------******Alphas*OnInit******--------//start'
CostomClass.OnInitStr = ''
CostomClass.OnInitEnd   = '//--------******Alphas*OnInit******--------//end'
function CostomClass.reset()
	CostomClass.wndName = ''
	CostomClass.wndViewName = ''
	CostomClass.CostomFuntionStr = ''
	CostomClass.OnInitStr = ''
end
function CostomClass.paiseCostomClass(handler,classInfo,_ExportSetting)
		CostomClass.reset()
		local getMemberByName = _ExportSetting.settings.getMemberByName
		fprint("getMemberByName = "..tostring(getMemberByName))
		local typeclass = TypeParse.ParseClass(classInfo)
		--if strIndex ~= nil then 
			local writer = CodeWriter.new()
			local wndName = typeclass
			local wndViewName = wndName
			CostomClass.wndName = wndName
			CostomClass.wndViewName = wndViewName
			fprint("wndName = "..tostring(wndName))
			local members = classInfo.members
			writer:reset()
			CostomClass.getCostomStr(classInfo,_ExportSetting)
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

			if handler.project.type==ProjectType.MonoGame then
				writer:writeln("protected override void OnConstruct()")
				writer:startBlock()
			else
				writer:writeln('public override void ConstructFromXML(XML xml)')
				writer:startBlock()
				writer:writeln('base.ConstructFromXML(xml);')
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
			table.insert(writer.lines, CostomClass.OnInitStart)
			table.insert(writer.lines, CostomClass.OnInitStr)
			table.insert(writer.lines, CostomClass.OnInitEnd)
			writer:endBlock()
			
			--不可更改区域
			table.insert(writer.lines, CostomClass.CostomFuntionStart)
			table.insert(writer.lines, CostomClass.CostomFuntionStr)
			table.insert(writer.lines, CostomClass.CostomFuntionEnd)
			writer:endBlock() --class
			writer:endBlock() --namepsace
			writer:save(_ExportSetting.exportCodeViewPath..'/'..CostomClass.wndViewName..'.cs')

	--	end
		return CostomClass
end

function CostomClass.getCostomStr(classInfo,_ExportSetting)
	local wndPath = _ExportSetting.exportCodeViewPath..'/'..CostomClass.wndViewName..'.cs'
	if File.Exists(wndPath) == true then
		local file = io.open(wndPath,"r")
		local luaFileStr = file:read("*a")
		file:close()
		CostomClass.CostomFuntionStr = TypeParse.getCostomStr(luaFileStr,CostomClass.CostomFuntionStart,CostomClass.CostomFuntionEnd)
		CostomClass.OnInitStr = TypeParse.getCostomStr(luaFileStr,CostomClass.OnInitStart,CostomClass.OnInitEnd)
	end
end


return CostomClass