import {DID_TOKEN_KEY} from "../constants/constants";

export const getPartOfIdToShow = (): string => {
  let didToken = localStorage.getItem(DID_TOKEN_KEY);
  if (!didToken) {
    didToken = 'Authenticated';
  } else {
    try {
      didToken = didToken.slice(0, 15);
      didToken += '...';
    } catch {
    }
  }
  return didToken;
}


// function getBase64Antd(img: any, callback: any) {
//     const reader = new FileReader();
//     reader.addEventListener('load', () => callback(reader.result));
//     reader.readAsDataURL(img);
// }
//
// function beforeUpload(file: any) {
//     const isJpgOrPng = file.type === 'image/jpeg' || file.type === 'image/png';
//     if (!isJpgOrPng) {
//         message.error('You can only upload JPG/PNG file!');
//     }
//     const isLt2M = file.size / 1024 / 1024 < 2;
//     if (!isLt2M) {
//         message.error('Image must smaller than 2MB!');
//     }
//     return isJpgOrPng && isLt2M;
// }

export const getBase64 = (file:any) => {
  console.log('get base 64');
  return new Promise((resolve,reject) => {
    const reader = new FileReader();
    reader.onload = () => resolve(reader.result);
    reader.onerror = error => reject(error);
    reader.readAsDataURL(file);
  });
}
