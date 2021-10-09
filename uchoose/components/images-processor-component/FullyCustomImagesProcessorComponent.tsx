import React, {useEffect, useRef, useState} from "react";
import {fabric} from 'fabric';
import styles from "../../styles/images-processor-component/images-processor-component.module.css";
import {CIRCLE, FILL, LINE, RECTANGLE, STROKE, TEXT} from "./DeafultShapesComponent";
import {getBase64} from "../../utils/utils";

// http://fabricjs.com/events
// http://fabricjs.com/articles/
// https://stackoverflow.com/questions/33940313/how-to-restrict-rectangle-resizing-moving-outside-a-image-in-fabricjs
// https://stackoverflow.com/questions/20756042/how-to-display-an-image-stored-as-byte-array-in-html-javascript


// TODO. to add custom properties if needed
// https://stackoverflow.com/questions/30336983/adding-custom-attributes-to-fabricjs-object

interface FabricJSEditor {
    canvas: fabric.Canvas
    addCircle: () => void
    addRectangle: () => void
    addLine: () => void
    addText: (text: string) => void
    updateText: (text: string) => void
    deleteAll: () => void
    deleteSelected: () => void
    fillColor: string
    strokeColor: string
    setFillColor: (color: string) => void
    setStrokeColor: (color: string) => void
    zoomIn: () => void
    zoomOut: () => void
}

/**
 * Creates editor
 */
const buildEditor = (
    canvas: fabric.Canvas,
    fillColor: string,
    strokeColor: string,
    _setFillColor: (color: string) => void,
    _setStrokeColor: (color: string) => void,
    scaleStep: number
): FabricJSEditor => {
    return {
        canvas,
        addCircle: () => {
            const object = new fabric.Circle({
                ...CIRCLE,
                fill: fillColor,
                stroke: strokeColor
            })
            canvas.add(object)
        },
        addRectangle: () => {
            const object = new fabric.Rect({
                ...RECTANGLE,
                fill: fillColor,
                stroke: strokeColor
            })
            canvas.add(object)
        },
        addLine: () => {
            const object = new fabric.Line(LINE.points, {
                ...LINE.options,
                stroke: strokeColor
            })
            canvas.add(object)
        },
        addText: (text: string) => {
            // use stroke in text fill, fill default is most of the time transparent
            const object = new fabric.Textbox(text, {...TEXT, fill: strokeColor})
            object.set({text: text})
            canvas.add(object)
        },
        updateText: (text: string) => {
            const objects: any[] = canvas.getActiveObjects()
            if (objects.length && objects[0].type === TEXT.type) {
                const textObject: fabric.Textbox = objects[0]
                textObject.set({text})
                canvas.renderAll()
            }
        },
        deleteAll: () => {
            canvas.getObjects().forEach((object) => canvas.remove(object))
            canvas.discardActiveObject()
            canvas.renderAll()
        },
        deleteSelected: () => {
            canvas.getActiveObjects().forEach((object) => canvas.remove(object))
            canvas.discardActiveObject()
            canvas.renderAll()
        },
        fillColor,
        strokeColor,
        setFillColor: (fill: string) => {
            _setFillColor(fill)
            canvas.getActiveObjects().forEach((object) => object.set({fill}))
            canvas.renderAll()
        },
        setStrokeColor: (stroke: string) => {
            _setStrokeColor(stroke)
            canvas.getActiveObjects().forEach((object) => {
                if (object.type === TEXT.type) {
                    // use stroke in text fill
                    object.set({fill: stroke})
                    return
                }
                object.set({stroke})
            })
            canvas.renderAll()
        },
        zoomIn: () => {
            const zoom = canvas.getZoom()
            canvas.setZoom(zoom / scaleStep)
        },
        zoomOut: () => {
            const zoom = canvas.getZoom()
            canvas.setZoom(zoom * scaleStep)
        }
    }
}

interface FabricJSEditorState {
    editor?: FabricJSEditor
}

export interface Props {
    className?: string
    onReady?: (canvas: fabric.Canvas) => void
    editorWidth: number
    editorHeight: number
}

/**
 * Fabric canvas as component
 */
export const FullyCustomImagesProcessorComponent = ({className, onReady, editorWidth, editorHeight}: Props) => {
    const scaleStep = 1;
    const defaultFillColor: string = 'rgba(255, 255, 255, 0.0)';
    const defaultStrokeColor: string = 'rgba(255, 255, 255, 0.0)';
    const [canvas, setCanvas] = useState<null | fabric.Canvas>(null);
    const [fillColor, setFillColor] = useState<string>(defaultFillColor || FILL);
    const [strokeColor, setStrokeColor] = useState<string>(
        defaultStrokeColor || STROKE
    );
    const canvasEl = useRef(null);
    const canvasElParent = useRef<HTMLDivElement>(null)
    const [selectedObjects, setSelectedObject] = useState<fabric.Object[]>([]);
    const [editor, setEditor] = useState(null);

    const [currentLoadedImage, setCurrentLoadedImage] = useState<null | string>(null);

    // TODO. Get info about parts/properties of image

    useEffect(() => {
        const canvas = new fabric.Canvas(canvasEl.current);
        const setCurrentDimensions = () => {
            canvas.setHeight(canvasElParent.current?.clientHeight || 0)
            canvas.setWidth(canvasElParent.current?.clientWidth || 0)
            canvas.renderAll()
        }
        const resizeCanvas = () => {
            setCurrentDimensions();
        }
        setCurrentDimensions();

        window.addEventListener('resize', resizeCanvas, false);

        console.log('%c init canvas', 'background-color: red');
        console.log(canvas);
        setCanvas(canvas);
        // onReady: (canvasReady: fabric.Canvas): void => {
        //         console.log('Fabric canvas ready')
        //         setCanvas(canvasReady)
        //     },
        // if (onReady) {
        //     onReady(canvas)
        // }
        // let canvasReady: fabric.Canvas
        return () => {
            canvas.dispose();
            window.removeEventListener('resize', resizeCanvas);
        }
    }, [])

    useEffect(() => {
        const bindEvents = (canvas: fabric.Canvas) => {
            canvas.on('selection:cleared', () => {
                setSelectedObject([])
            })
            canvas.on('selection:created', (e: any) => {
                setSelectedObject(e.selected)
            })
            canvas.on('selection:updated', (e: any) => {
                setSelectedObject(e.selected)
            })
            // canvas.on()
        }
        console.log('canvas???');
        if (canvas) {
            console.log('initializing editor');
            bindEvents(canvas);
            console.log('initialization finished');
        }
    }, [canvas])


    const imageUpload = (e: any, imageName: string) => {
        console.log('image upload');
        console.log(e);
        console.log('---');
        const file = e.target.files[0];
        getBase64(file).then(base64 => {
            // @ts-ignore
            setCurrentLoadedImage(base64);
            fabric.Image.fromURL(base64 as string, function (oImg) {
                // @ts-ignore
                oImg.on("selected", function () {
                    console.log('it\'s selected')
                });
                canvas?.add(oImg);
                console.log(oImg);
            });
            canvas?.on('object:moving', function (options: any) {
                console.log(options.e.clientX, options.e.clientY);
                console.log(options);
                console.log(imageName);
            });
        });

        console.log(canvas);
        console.log(canvas?.getActiveObject());
        console.log(canvas?.getObjects());
        // clear files value in input after load
        // e.target.value = null;
    };

    const printImageData = () => {
        console.log('%c --- printing canvas state to console ---', 'background-color: red');
        console.log(canvas);
        console.log(canvas?.getActiveObject());
        console.log(canvas?.getObjects());
        console.log('%c ---', 'background-color: red');
    }

    const clearCanvas = () => {
        console.log('%c --- canvas cleaned ---');
        canvas?.clear();
    }

    const saveToJson = () => {
        console.log('%c ---', 'background-color: yellow');
        // @ts-ignore
        const test1 = canvas?.toJSON();
        console.log(test1);
        console.log('%c ---', 'background-color: yellow');
        // @ts-ignore
        const test2 = canvas?.toDatalessJSON();
        console.log(test2);
        console.log('%c ---', 'background-color: yellow');

        clearCanvas();
        setTimeout(() => {
            console.log('trying to restore canvas');
            canvas?.loadFromJSON(test2, canvas.renderAll.bind(canvas));
        }, 3000);
        // canvas?.
    }

    return (
        <div style={{marginTop: '30px', marginBottom: '100px'}}>
            <div>
                <h1 style={{textAlign: 'center'}}>NFT Editor panel</h1>
                <hr/>
            </div>
            {/*Canvas. Left Part*/}
            <div style={{display: 'inline-block', width: editorWidth, height: editorHeight}}>
                <p>Canvas width = {editorWidth}, height = {editorHeight}</p>
                <div ref={canvasElParent} className={styles.drawingCanvasArea}
                     style={{width: editorWidth, height: editorHeight}}>
                    <canvas ref={canvasEl}/>
                </div>
            </div>
            {/*Images settings. Right Part*/}
            <div className={styles.NFTSettingsContainer} style={{width: editorWidth, height: editorHeight}}>
                <p>NFT Image settings</p>
                <p>Image parts</p>
                <div>
                    <label htmlFor="file-upload-head" className={styles.customFileUpload}>
                        <i className="bi bi-image"/> Add Head
                    </label>
                    <input id="file-upload-head" type="file" className={styles.customFileUploadInput}
                           onChange={(e) => imageUpload(e, 'head')}/>
                </div>
                <div>
                    <label htmlFor="file-upload-body" className={styles.customFileUpload}>
                        <i className="bi bi-image"/> Add body
                    </label>
                    <input id="file-upload-body" type="file" className={styles.customFileUploadInput}
                           onChange={(e) => imageUpload(e, 'body')}/>
                </div>
                <div>
                    <button onClick={printImageData}>Print data to console (for devs)</button>
                </div>
                <div>
                    <label htmlFor="return-to-clean-canvas" className={styles.customFileUpload}>
                        <i className="bi bi-check"/> Save to json
                    </label>
                    <input id="return-to-clean-canvas" className={styles.customFileUploadInput}
                           onClick={saveToJson}/>
                </div>
                <div style={{marginTop: '50px'}}>
                    <label htmlFor="return-to-clean-canvas" className={styles.customFileUpload}>
                        <i className="bi bi-bucket"/> Return to clean canvas
                    </label>
                    <input id="return-to-clean-canvas" className={styles.customFileUploadInput}
                           onClick={clearCanvas}/>
                </div>
            </div>
        </div>
    )
}

// return {
//     selectedObjects,
//     // onReady: (canvasReady: fabric.Canvas): void => {
//     //     console.log('Fabric canvas ready')
//     //     setCanvas(canvasReady)
//     // },
//     editor: canvas
//         ? buildEditor(
//             canvas,
//             fillColor,
//             strokeColor,
//             setFillColor,
//             setStrokeColor,
//             scaleStep
//         )
//         : undefined
// }

// const onAddImageFromUrl = () => {
//     // fabric.Image.fromURL("https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png", function (oImg) {
//     //     editor?.canvas.add(oImg);
//     //     editor?.canvas.on('mouse:down', function (options: any) {
//     //         console.log(options.e.clientX, options.e.clientY);
//     //     });
//     //     editor?.canvas.on('mouse:move', function (options: any) {
//     //         console.log(options.e.clientX, options.e.clientY);
//     //     });
//     // });
//     fabric.Image.fromURL('https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png', function (oImg) {
//         // @ts-ignore
//         canvas.add(oImg);
//     });
//     // @ts-ignore
//     console.log(editor?.canvas);
// };
//
// const onAddImage = () => {
//     fabric.Image.fromURL('https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png', function (oImg) {
//         // @ts-ignore
//         canvas.add(oImg);
//     });
//     // @ts-ignore
//     console.log(editor?.canvas);
// };
