# ProxyHandler

```
最新的jsmod2协议代理插件，请开发此来适应最新的jsmod2 v4协议

The latest jsmod2 protocol proxy plugin, please develop this to adapt to the latest jsmod2 v4 protocol
```

ProxyHandler是一款解析jsmod2协议的解析器，以映射到multiAdmin，实现游戏中的调用，从而实现任何语言通过jsmod2协议操控
multiAdmin.

ProxyHandler作为一个Smod2插件运行在multiAdmin，并确保在启动jsmod2后再启动ProxyHandler

目前ProxyHandler在开发阶段，已经实现了部分功能，其他功能待实现

!!!!ProxyHandler目前已经实现!!!!

`总体[||||||||||||                                        ]` %52

1. 响应Jsmod2的事件监听器(%30)

2.已经实现解析实体的协议:
```
    
    Map (%0)
    PocketDimensionExit (%0)
    Room (%0)
    TeslaGate (%0)
    Player (%0)
    Scp079Data(%0)
    Connection (%0)
    Round (%0)
    RoundStats (%0)
    TeamRole(%0)
    UserGroup(%0)
    
    Finished:
    Item (%100)
    Door (%100)
    (Smod2)Server (%100)
    Elevator (%100)
    Generator (%100)
    Event(%100)
    
``` 
3. 对于一些特殊情况的处理

如Component GameObject等Object类型，目前暂不写，未来将修复他们
    
