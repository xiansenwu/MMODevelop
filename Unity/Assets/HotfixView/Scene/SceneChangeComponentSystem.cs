﻿using UnityEngine.SceneManagement;

namespace ET
{
    public class SceneChangeComponentUpdateSystem: UpdateSystem<SceneChangeComponent>
    {
        public override void Update(SceneChangeComponent self)
        {
            if (self.loadMapOperation == null)
            {
                return;
            }
            if (!self.loadMapOperation.isDone)
            {
                return;
            }

            if (self.tcs == null)
            {
                return;
            }
            
            ETTaskCompletionSource tcs = self.tcs;
            self.tcs = null;
            tcs.SetResult();
        }
    }
	
    
    public class SceneChangeComponentDestroySystem: DestroySystem<SceneChangeComponent>
    {
        public override void Destroy(SceneChangeComponent self)
        {
            self.loadMapOperation = null;
            self.tcs = null;
        }
    }

    public static class SceneChangeComponentSystem
    {
        public static async ETTask ChangeSceneAsync(this SceneChangeComponent self, string sceneName)
        {
            self.tcs = new ETTaskCompletionSource();
            // 加载map
            self.loadMapOperation = SceneManager.LoadSceneAsync(sceneName);
            //this.loadMapOperation.allowSceneActivation = false;
            await self.tcs.Task;
        }
        
        public static int Process(this SceneChangeComponent self)
        {
            if (self.loadMapOperation == null)
            {
                return 0;
            }
            return (int)(self.loadMapOperation.progress * 100);
        }
    }
}