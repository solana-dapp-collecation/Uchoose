import React, {FC} from 'react';

import {styles} from '../../styles/phantom-connector/notification.module.css';

export interface NotificationProps {
    message: string;
    variant: 'Error' | 'Info' | 'Success';
}

const Notification: FC<NotificationProps> = ({message, variant}) => {
    return <div className={`${styles.walletNotification} wallet-notification-${variant}`}>{message}</div>;
};

export default Notification;
