export default class Data {

  constructor (options) {
    this.store = options.store
  }

  get items () {
    var data = this.store.getters.entity
    var items = []
    if (data && data.properties) {
      var keys = Object.keys(data.properties)
      keys.forEach((key) => {
        if (!key.startsWith('_')) {
          var properties = this._getProperties(key)
          var hidden = properties.hasOwnProperty('hidden') ? properties.hidden : false
          if (properties && !hidden) {
            items.push({
              title: properties.title,
              value: data.properties[key]
            })
          }
        }
      })
    }
    return items
  }

  get dataHeaders () {
    var entity = this.store.getters.entity
    if (entity && entity.hasSubEntityByRel('dataHeader')) {
      return entity.getSubEntitiesByRel('dataHeader')
    }

    return []
  }

  _getProperties (name) {
    var dataHeaders = this.dataHeaders
    if (dataHeaders && dataHeaders.length > 0) {
      var dataHeader = dataHeaders.find(dataHeader => dataHeader.properties.name === name)
      if (dataHeader) {
        return dataHeader.properties
      }
    }
  }

  hasActions () {
    var exist = this.validItems.filter(entity => entity.hasAction())
    return exist && exist.length > 0
  }

}