local Directory = CS.System.IO.Directory;
local File = CS.System.IO.File;

TypeParse = {}
function TypeParse.ParseClass(classInfo)
		local strIndex =  string.find(classInfo.className,'_')
		
		if strIndex ~= nil then 
			return string.sub(classInfo.className,strIndex+1)
		end
		return classInfo.className
end
function TypeParse.ParseMembers(memberInfo)
	if memberInfo.res ~= nil then
		--TypeParse.printResInfo(memberInfo.res)
		local strresIndex =  string.find(memberInfo.res.name,'_')
		if strresIndex ~= nil then 
			local memberType = string.sub(memberInfo.res.name,strresIndex+1)
			return memberType
		end
	end
	return memberInfo.type
end
function TypeParse.getCostomStr(costomStr,startstr,endstr,nnnname)
				local strStartIndex =  string.find(costomStr,startstr,1,true)
				local strEndIndex =  string.find(costomStr,endstr,1,true)
				
				if strStartIndex ~= nil and strEndIndex ~= nil then
					strStartIndex = strStartIndex + string.len(startstr) +1 --(+1是去除\n)
					strEndIndex = strEndIndex - 1 -1 --(-1去除自身  -1是去除\n)
					local str = ''
					local beforestr2 = ''
					local endstr2 = ''
					if strEndIndex - strStartIndex > 0 then
						str = string.sub(costomStr,strStartIndex,strEndIndex)
						--fprint("CostomFuntionStr ="..CostomFuntionStr.."-")
					end
					if strEndIndex - strStartIndex >= -1 then
						beforestr2 = string.sub(costomStr,1,strStartIndex-1-1)
						endstr2 = string.sub(costomStr,strEndIndex+1+1,string.len(costomStr))
					end
					return str,beforestr2,endstr2
				end
				return '','',''
end

function TypeParse.IsInTable(value,tbl)
	for k,v in ipairs(tbl) do
		if v == value then
			return true
		end
	end
	return false
end
function TypeParse.printMembers(Members)
	fprint("Members.name = "..tostring(Members.name))
	fprint("Members.varName = "..tostring(Members.varName))
	fprint("Members.type = "..tostring(Members.type))
	fprint("Members.res = "..tostring(Members.res))
	fprint("Members.group = "..tostring(Members.group))
end
function TypeParse.printResInfo(res)
	if res == nil then return end
	fprint("res.owner = "..tostring(res.owner))
	fprint("res.parent = "..tostring(res.parent))
	fprint("res.type = "..tostring(res.type))
	fprint("res.name = "..tostring(res.name))
	fprint("res.fileName = "..tostring(res.fileName))
	fprint("res.title = "..tostring(res.title))
end

return TypeParse