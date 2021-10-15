import React, {useEffect, useRef, useState} from "react";
import {fabric} from 'fabric';
import styles from "../assets/styles/nft-editor-component/nft-editor-component.module.css";
import {FILL, STROKE} from "./DeafultShapesComponent";
import {getBase64} from "../utils/utils";
//@ts-ignore
import uuid from 'react-uuid';

interface Props {
    className?: string
    onReady?: (canvas: fabric.Canvas) => void
    editorWidth: number
    editorHeight: number
}

interface ILayer {
    id: string,
    name: string,
    src: string | null,
}

/**
 * Fabric canvas as component
 */
export const NFTEditorComponent = ({className, onReady, editorWidth, editorHeight}: Props) => {
    const scaleStep = 1;
    const defaultFillColor: string = 'rgba(155, 155, 155, 0.5)';
    const defaultStrokeColor: string = 'rgba(255, 255, 255, 0.0)';
    const [canvas, setCanvas] = useState<null | fabric.Canvas>(null);
    const [fillColor, setFillColor] = useState<string>(defaultFillColor || FILL);
    const [backgroundColor, setBackgroundColor] = useState<string>('#FFFFFF');
    const [strokeColor, setStrokeColor] = useState<string>(
        defaultStrokeColor || STROKE
    );
    const canvasEl = useRef(null);
    const canvasElParent = useRef<HTMLDivElement>(null)
    const [selectedObjects, setSelectedObject] = useState<fabric.Object[]>([]);
    const [editor, setEditor] = useState(null);

    const [currentLoadedImage, setCurrentLoadedImage] = useState<null | string>(null);
    const [canvasStateBeforeSave, setCanvasStateBeforeSave] = useState<undefined | string>();

    const [listOfLayers, setListOfLayers] = useState<Array<ILayer>>([]);

    // TODO. Get info about parts/properties of image
    useEffect(() => {
        console.log('use effect invoked');
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

        canvas.backgroundColor = backgroundColor;
        canvas.renderAll();
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
        if (canvas) {
            bindEvents(canvas);
        }
    }, [canvas])

    const imageUpload = (e: any, imageName: string) => {
        console.log('image upload');
        console.log(e);
        console.log('---');
        if (!e || !e.target || !e.target.files[0]) {
            console.log('File is empty');
            return;
        }
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
        console.log(JSON.stringify(test2));
        console.log('%c ---', 'background-color: yellow');
        setCanvasStateBeforeSave(test1);
    }

    const restoreCanvasFromJson = () => {
        console.log('%c --- restoring canvas --- ', 'background-color: yellow');
        clearCanvas();
        setTimeout(() => {
            console.log('trying to restore canvas');
            canvas?.loadFromJSON(canvasStateBeforeSave, canvas.renderAll.bind(canvas));
        }, 100);
        console.log('%c ---', 'background-color: yellow');
    }

    const onChangePalette = (color: any) => {
        canvas!.backgroundColor = color;
        canvas?.renderAll();
    }

    const renderLayersPart = () => {
        if (listOfLayers.length === 0) {
            return (
                <>
                    Click button above to add new layers
                </>
            )
        }
        return listOfLayers.map((layerItem: any, index: number) => {
            return <div key={layerItem.id} style={{marginTop: '10px'}}>
                <label htmlFor={`${layerItem.id}_add_image`} className={styles.customFileUpload}>
                    <i className="bi bi-image"/> Add body
                </label>
                <input type="text"/>
                <input id={`${layerItem.id}_add_image`} type="file" className={styles.customFileUploadInput}
                       onChange={(e) => imageUpload(e, 'body')}/>
                <label htmlFor={`${layerItem.id}_remove_layer`} className={styles.removeButton}
                       style={{marginLeft: '10px'}}>
                    <i className="bi bi-bucket"/>
                </label>
                <input id={`${layerItem.id}_remove_layer`} type="button" className={styles.displayNone}
                       onClick={(e) => removeLayer(layerItem.id)}/>
            </div>
        });
    }

    const addLayer = () => {
        let obj: ILayer = {
            id: uuid(),
            name: '',
            src: null
        }
        let tempArray = [...listOfLayers];
        tempArray.push(obj);
        setListOfLayers([...tempArray]);
    }

    const removeLayer = (layerId: string) => {
        let tempArray = listOfLayers.filter((item) => item.id !== layerId);
        setListOfLayers([...tempArray]);
    }

    return (
        <div style={{marginTop: '30px', marginBottom: '100px'}}>
            <div>
                <h1 style={{textAlign: 'center'}}>NFT Editor panel</h1>
                <hr/>
            </div>
            {/*Canvas. Left Part*/}
            <div style={{display: 'inline-block', width: editorWidth, height: editorHeight}}>
                <p>Canvas width = {editorWidth}, height = {editorHeight}. (Fixed for testing)</p>
                <div ref={canvasElParent} className={styles.drawingCanvasArea}
                     style={{width: editorWidth, height: editorHeight}}>
                    <canvas ref={canvasEl}/>
                </div>
            </div>
            {/*Images settings. Right Part*/}
            <div className={styles.NFTSettingsContainer} style={{width: editorWidth, height: editorHeight}}>
                <p><b>NFT Image settings</b></p>
                <div>
                    <label htmlFor="add-layer-button" className={`${styles.customFileUpload} ${styles.addLayer}`}>
                        <i className="bi bi-plus-circle" style={{color: 'white'}}/> Add layer
                    </label>
                    <input id="add-layer-button" className={styles.customFileUploadInput}
                           onClick={addLayer}/>
                </div>
                <div>
                    {renderLayersPart()}
                </div>

                {/*<div>*/}
                {/*    <label htmlFor="file-upload-head" className={styles.customFileUpload}>*/}
                {/*        <i className="bi bi-image"/> Add Head*/}
                {/*    </label>*/}
                {/*    <input id="file-upload-head" type="file" className={styles.customFileUploadInput}*/}
                {/*           onChange={(e) => imageUpload(e, 'head')}/>*/}
                {/*</div>*/}
                <div style={{marginTop: '30px'}}>
                    <label htmlFor="set-background-color" className={styles.customFileUpload}>
                        Set background color
                    </label>
                    <input style={{marginLeft: '10px', paddingTop: '2px', height: '20px'}} type="color"
                           defaultValue={backgroundColor}
                           onChange={(e) => onChangePalette(e.target.value)}/>
                </div>

                <div style={{}}>
                    <label htmlFor="save-to-json" className={styles.customFileUpload}>
                        <i className="bi bi-check"/> Create NFT image image
                    </label>
                    <input id="save-to-json" className={styles.customFileUploadInput}
                           onClick={saveToJson}/>
                </div>
                <div style={{marginTop: '30px'}}>
                    <label htmlFor="restore-canvas" className={styles.customFileUpload}>
                        <i className="bi bi bi-arrow-counterclockwise"/> Restore
                    </label>
                    <input id="restore-canvas" className={styles.customFileUploadInput}
                           onClick={restoreCanvasFromJson}/>
                </div>
                <div>
                    <label htmlFor="return-to-clean-canvas" className={styles.customFileUpload}>
                        <i className="bi bi-bucket"/> Return to clean canvas
                    </label>
                    <input id="return-to-clean-canvas" className={styles.customFileUploadInput}
                           onClick={clearCanvas}/>
                </div>
                <div style={{marginTop: '10px'}}>
                    <button onClick={printImageData}>Print data to console (for devs)</button>
                </div>
            </div>
        </div>
    )
}
