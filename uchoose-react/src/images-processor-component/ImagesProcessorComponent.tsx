import React, {useEffect} from "react";
import {fabric} from 'fabric';
import {FabricJSCanvas, useFabricJSEditor} from "fabricjs-react";
import styles from "../../styles/images-processor-component/images-processor-component.module.css";

export default function ImagesProcessorComponent() {
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
    }, []);

    const onAddCircle = () => {
        editor.addCircle();
    };

    const onAddRectangle = () => {
        editor.addRectangle();
    };

    const onAddImage = () => {
        fabric.Image.fromURL("https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png", function (oImg) {
            editor?.canvas.add(oImg);
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
