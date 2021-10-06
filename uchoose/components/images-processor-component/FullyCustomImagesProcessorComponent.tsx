import React, {useEffect, useRef, useState} from "react";
import {fabric} from 'fabric';
import styles from "../../styles/images-processor-component/images-processor-component.module.css";
import {CIRCLE, FILL, LINE, RECTANGLE, STROKE, TEXT} from "./DeafultShapesComponent";

// http://fabricjs.com/events
// http://fabricjs.com/articles/
// https://stackoverflow.com/questions/33940313/how-to-restrict-rectangle-resizing-moving-outside-a-image-in-fabricjs
// https://stackoverflow.com/questions/20756042/how-to-display-an-image-stored-as-byte-array-in-html-javascript

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

interface FabricJSEditorHook extends FabricJSEditorState {
    selectedObjects?: fabric.Object[]
    onReady: (canvas: fabric.Canvas) => void
}

interface FabricJSEditorHookProps {
    defaultFillColor?: string
    defaultStrokeColor?: string
    scaleStep?: number
}

export interface Props {
    className?: string
    onReady?: (canvas: fabric.Canvas) => void
}

/**
 * Fabric canvas as component
 */
export const FullyCustomImagesProcessorComponent = ({className, onReady}: Props) => {
    const scaleStep = 1;
    const defaultFillColor: string = 'rgba(255, 255, 255, 0.0)';
    const defaultStrokeColor: string = 'rgba(255, 255, 255, 0.0)';
    const [canvas, setCanvas] = useState<null | fabric.Canvas>(null);
    const [fillColor, setFillColor] = useState<string>(defaultFillColor || FILL);
    const [strokeColor, setStrokeColor] = useState<string>(
        defaultStrokeColor || STROKE
    );
    const canvasEl = useRef(null)
    const canvasElParent = useRef<HTMLDivElement>(null)
    const [selectedObjects, setSelectedObject] = useState<fabric.Object[]>([]);
    const [editor, setEditor] = useState(null);

    useEffect(() => {
        const canvas = new fabric.Canvas(canvasEl.current)
        const setCurrentDimensions = () => {
            canvas.setHeight(canvasElParent.current?.clientHeight || 0)
            canvas.setWidth(canvasElParent.current?.clientWidth || 0)
            canvas.renderAll()
        }
        const resizeCanvas = () => {
            setCurrentDimensions()
        }
        setCurrentDimensions()

        window.addEventListener('resize', resizeCanvas, false)


        console.log('%c init canvas','background-color: red');
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
            canvas.dispose()
            window.removeEventListener('resize', resizeCanvas)
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
            // const initalizedEditor = buildEditor(
            //     canvas,
            //     fillColor,
            //     strokeColor,
            //     setFillColor,
            //     setStrokeColor,
            //     scaleStep
            // );
            // @ts-ignore
            // setEditor(initalizedEditor);
            console.log('initialization finished');
        }
    }, [canvas])

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

    const onAddCircle = () => {
        // @ts-ignore
        canvas.add();
    };

    const onAddRectangle = () => {
        // @ts-ignore
        editor.addRectangle();
    };

    const onAddImage = () => {
        // fabric.Image.fromURL("https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png", function (oImg) {
        //     editor?.canvas.add(oImg);
        //     editor?.canvas.on('mouse:down', function (options: any) {
        //         console.log(options.e.clientX, options.e.clientY);
        //     });
        //     editor?.canvas.on('mouse:move', function (options: any) {
        //         console.log(options.e.clientX, options.e.clientY);
        //     });
        // });
        fabric.Image.fromURL('https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png', function(oImg) {
            // @ts-ignore
            canvas.add(oImg);
        });
        // @ts-ignore
        console.log(editor?.canvas);
    };

    return (
        <div ref={canvasElParent} className={styles.drawingCanvasArea}>
            <button onClick={onAddCircle}>Add circle</button>
            <button onClick={onAddRectangle}>Add Rectangle</button>
            <button onClick={onAddImage}>Add Image</button>
            <canvas ref={canvasEl}/>
        </div>
    )
}

// export default function FullyCustomImagesProcessorComponent() {
//
//     useEffect(() => {
//         // const bindEvents = (canvas: fabric.Canvas) => {
//         //     canvas.on('selection:cleared', () => {
//         //         setSelectedObject([])
//         //     })
//         //     canvas.on('selection:created', (e: any) => {
//         //         setSelectedObject(e.selected)
//         //     })
//         //     canvas.on('selection:updated', (e: any) => {
//         //         setSelectedObject(e.selected)
//         //     })
//         // }
//         // if (canvas) {
//         //     bindEvents(canvas)
//         // }
//         console.log(editor?.canvas);
//         initCanvasData();
//     }, []);
//
//     const initCanvasData = () => {
//         console.log(editor);
//     }
//
//     const onAddCircle = () => {
//         editor.addCircle();
//     };
//
//     const onAddRectangle = () => {
//         editor.addRectangle();
//     };
//
//     const onAddImage = () => {
//         fabric.Image.fromURL("https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png", function (oImg) {
//             editor?.canvas.add(oImg);
//             editor?.canvas.on('mouse:down', function (options: any) {
//                 console.log(options.e.clientX, options.e.clientY);
//             });
//             editor?.canvas.on('mouse:move', function (options: any) {
//                 console.log(options.e.clientX, options.e.clientY);
//             });
//         });
//         console.log(editor?.canvas);
//     };
//
//     return (
//         <div>
//             <h1>FabricJS React Sample</h1>
//             <button onClick={onAddCircle}>Add circle</button>
//             <button onClick={onAddRectangle}>Add Rectangle</button>
//             <button onClick={onAddImage}>Add Image</button>
//             <FabricJSCanvas className={styles.drawingCanvasArea} onReady={onReady}/>
//         </div>
//     );
// }
