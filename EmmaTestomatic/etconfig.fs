module etconfig
open FSharp.Configuration

type ETConfig = YamlConfig<"config.yaml">
let config = ETConfig()

let mutable baseEmmaUrl = config.baseEmmaUrl.ToString()
let mutable baseDataportUrl = config.baseDataportUrl.ToString()
