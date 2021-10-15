import React from 'react';
import tagsCloudStyles from '../assets/styles/tags-cloud-component/tags-cloud-component.module.css';

const CloudItem = (props: any) => (
    <div {...props} className={tagsCloudStyles.tagItemWrapper}>
        <div>
            {props.text}
        </div>
        <div className={tagsCloudStyles.tagItemTooltip}>
            HOVERED!
        </div>
    </div>
);

export default CloudItem;
