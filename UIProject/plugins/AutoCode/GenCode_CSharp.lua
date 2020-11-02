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
local TypeParse = RequireMod('/plugins/AutoCode/TypeParse.lua')
local Window = RequireMod('/plugins/AutoCode/Window.lua')
local CostomClass = RequireMod('/plugins/AutoCode/CostomClass.lua')
ExportSetting = {}
ExportSetting.GeneralClass = {'GButton','GProgressBar',"GLabel","GComboBox","GSlider","BitmapFont","MovieClip",
	'Controller','GComponent','GImage','GTextField',"GRichTextField","GTextInput","GList","GLoader","GLoader3D","GGraph","GGroup"}
local function genCode(handler)
	--设置数据
	ExportSetting.handler = handler
    ExportSetting.settings = handler.project:GetSettings("Publish").codeGeneration
    ExportSetting.codePkgName = handler:ToFilename(handler.pkg.name); --convert chinese to pinyin, remove special chars etc.
    ExportSetting.exportCodePath = handler.exportCodePath..'/'..ExportSetting.codePkgName
	ExportSetting.exportCodeViewPath = ExportSetting.exportCodePath.."/View"
    ExportSetting.namespaceName = ExportSetting.codePkgName
	
    fprint("settings.packageName = "..ExportSetting.settings.packageName..",namespaceName = "..ExportSetting.namespaceName)
    if ExportSetting.settings.packageName~=nil and ExportSetting.settings.packageName~='' then
        --namespaceName = settings.packageName..'.'..namespaceName;
		ExportSetting.namespaceName = ExportSetting.settings.packageName;
    end
	
    --CollectClasses(stripeMemeber, stripeClass, fguiNamespace)
	-- 类数据
    local classes = handler:CollectClasses(ExportSetting.settings.ignoreNoname, ExportSetting.settings.ignoreNoname, nil)
    --handler:SetupCodeFolder(exportCodePath, "cs") --check if target folder exists, and delete old files
	--目录创建
	if Directory.Exists(ExportSetting.exportCodePath) == false then
		Directory.CreateDirectory(ExportSetting.exportCodePath)
	end
	if Directory.Exists(ExportSetting.exportCodeViewPath) == false then
		Directory.CreateDirectory(ExportSetting.exportCodeViewPath)
	end
    local getMemberByName = ExportSetting.settings.getMemberByName

    local classCnt = classes.Count
	fprint("classCnt = "..classCnt)
    local writer = CodeWriter.new()
	--循环执行 所有类
    for i=0,classCnt-1 do
        local classInfo = classes[i]
		local typeclass = TypeParse.ParseClass(classInfo)
		fprint("typeclass = "..typeclass)
		local isSkip = TypeParse.IsInTable(typeclass,ExportSetting.GeneralClass)
		if isSkip == false then
			--window
			if typeclass == 'Window' then
				Window.paiseWindowClass(handler,classInfo,ExportSetting)
			else
				CostomClass.paiseCostomClass(handler,classInfo,ExportSetting)
			end
		end
		--[[
		--绑定拓展类
		local BindExtensionPath = ExportSetting.exportCodePath..'/BindExtension.cs'
		if File.Exists(BindExtensionPath) == false then
			writer:reset()
			local binderName = 'BindExtension'
			writer:writeln('using FairyGUI;')
			writer:writeln()
			writer:writeln('namespace %s', ExportSetting.namespaceName)
			writer:startBlock()
			writer:writeln('public class %s', binderName)
			writer:startBlock()
			writer:writeln('public static void BindAll()')
			writer:startBlock()
			for i=0,classCnt-1 do
				local classInfo = classes[i]
				local className = classInfo.className;
				local typeclass2 = TypeParse.ParseClass(classInfo)
				if typeclass2 == 'Window' then
				end
				writer:writeln('UIObjectFactory.SetPackageItemExtension(%s.URL, typeof(%s));', classInfo.className, className)
			end
			writer:endBlock() --bindall

			writer:endBlock() --class
			writer:endBlock() --namespace
			
			writer:save(ExportSetting.exportCodePath..'/../'..binderName..'.cs')
		else
		end]]--
    end
	
end

return genCode