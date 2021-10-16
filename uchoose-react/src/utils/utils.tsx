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

export const getBase64 = (file: any) => {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onload = () => resolve(reader.result);
        reader.onerror = error => reject(error);
        reader.readAsDataURL(file);
    });
}
