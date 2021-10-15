import React, {useEffect} from "react";
import {fabric} from 'fabric';
import {FabricJSCanvas, useFabricJSEditor} from "./FabricJsInitializer";
import styles from "../../styles/images-processor-component/images-processor-component.module.css";

// http://fabricjs.com/events
// http://fabricjs.com/articles/
// https://stackoverflow.com/questions/33940313/how-to-restrict-rectangle-resizing-moving-outside-a-image-in-fabricjs
// https://stackoverflow.com/questions/20756042/how-to-display-an-image-stored-as-byte-array-in-html-javascript
export default function CustomImagesProcessorComponent() {
    const {editor, onReady, selectedObjects}: any = useFabricJSEditor();

    useEffect(() => {
        // const bindEvents = (canvas: fabric.Canvas) => {
        //     canvas.on('selection:cleared', () => {
        //         setSelectedObject([])
        //     })
        //     canvas.on('selection:created', (e: any) => {
        //         setSelectedObject(e.selected)
        //     })
        //     canvas.on('selection:updated', (e: any) => {
        //         setSelectedObject(e.selected)
        //     })
        // }
        // if (canvas) {
        //     bindEvents(canvas)
        // }
        console.log(editor?.canvas);
        initCanvasData();
    }, []);

    const initCanvasData = () => {
        console.log(editor);
    }

    const onAddCircle = () => {
        editor.addCircle();
    };

    const onAddRectangle = () => {
        editor.addRectangle();
    };

    const onAddImage = () => {
        fabric.Image.fromURL("https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png", function (oImg) {
            editor?.canvas.add(oImg);
            editor?.canvas.on('mouse:down', function(options:any) {
                console.log(options.e.clientX, options.e.clientY);
            });
            editor?.canvas.on('mouse:move', function(options:any) {
                console.log(options.e.clientX, options.e.clientY);
                console.log(options);
            });
        });
        console.log(editor?.canvas);
    };

    return (
        <div>
            <h1>FabricJS React Sample</h1>
            <button onClick={onAddCircle}>Add circle</button>
            <button onClick={onAddRectangle}>Add Rectangle</button>
            <button onClick={onAddImage}>Add Image</button>
            <FabricJSCanvas className={styles.drawingCanvasArea} onReady={onReady}/>
        </div>
    );
}
