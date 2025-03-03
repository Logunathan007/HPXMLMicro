export function arrayToObjectTransformer(ele: any, name: string) {
  if (!Array.isArray(ele?.[name])) return;
  let opt = [...ele?.[name]];
  if (ele && ele?.[name]) {
    delete ele?.[name];
  }
  ele[name] = {};
  for (let obj of opt) {
    for (let [key, value] of Object.entries(obj)) {
      if (key !== 'options') {
        ele[name][key] = value;
      }
    }
  }
}
