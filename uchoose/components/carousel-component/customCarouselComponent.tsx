import React from "react";
import Section from "./section";
import WithScrollbar from "./withScrollbar";
import Carousel from "react-multi-carousel";
import "react-multi-carousel/lib/styles.css";
import {Card} from "antd";

// export function CarouselWithCards(props: React.PropsWithChildren<{}>) {
//     return (<Section>
//         <WithScrollbar/>
//     </Section>)
// }
const {Meta} = Card;

export default function CustomCarouselWithCards(props: React.PropsWithChildren<{}>) {
    const responsive = {
        desktop: {
            breakpoint: {max: 3000, min: 1024},
            items: 3,
            slidesToSlide: 3 // optional, default to 1.
        },
        tablet: {
            breakpoint: {max: 1024, min: 464},
            items: 2,
            slidesToSlide: 2 // optional, default to 1.
        },
        mobile: {
            breakpoint: {max: 464, min: 0},
            items: 1,
            slidesToSlide: 1 // optional, default to 1.
        }
    };
    return (<div style={{width: '1000px'}}>
            {/* https://www.npmjs.com/package/react-multi-carousel */}
            <Carousel
                swipeable={false}
                draggable={false}
                showDots={true}
                responsive={responsive}
                ssr={true}
                infinite={true}
                autoPlay={true}
                autoPlaySpeed={10000}
                keyBoardControl={true}
                customTransition="all .5"
                transitionDuration={300}
                containerClass="carousel-container"
                removeArrowOnDeviceType={["tablet", "mobile"]}
                //   deviceType={this.props.deviceType}
                dotListClass="custom-dot-list-style"
                itemClass="carousel-item-padding-40-px"
            >
                <div>
                    <Card
                        hoverable
                        // style={{ width: 240 }}
                        style={{
                            width: "90%",
                            cursor: "pointer",
                            marginLeft: '10px',
                            marginRight: '10px',
                            marginBottom: '30px'
                        }}
                        cover={<img alt="example" src="/pic_1_ex.jpg"/>}
                    >
                        <div>
                            Author: Noname
                            <br/>
                            Collections Params
                            <br/>
                            Price: 1$
                        </div>
                    </Card>
                    {/*<img*/}
                    {/*    draggable={false}*/}
                    {/*    style={{*/}
                    {/*        width: "90%",*/}
                    {/*        cursor: "pointer",*/}
                    {/*        marginLeft: '10px',*/}
                    {/*        marginRight: '10px',*/}
                    {/*        marginBottom: '30px'*/}
                    {/*    }}*/}
                    {/*    src="/pic_1_ex.jpg"*/}
                    {/*/>*/}
                </div>
                <div>
                    <Card
                        hoverable
                        style={{
                            width: "90%",
                            cursor: "pointer",
                            marginLeft: '10px',
                            marginRight: '10px',
                            marginBottom: '30px'
                        }}
                        cover={<img alt="example" src="/pic_2_ex.jpg"/>}
                    >
                        <div>
                            Author: Noname
                            <br/>
                            Collections Params
                            <br/>
                            Price: 2$
                        </div>
                    </Card>
                </div>
                <div>
                    <Card
                        hoverable
                        style={{
                            width: "90%",
                            cursor: "pointer",
                            marginLeft: '10px',
                            marginRight: '10px',
                            marginBottom: '30px'
                        }}
                        cover={<img alt="example" src="/pic_ex_3.png"/>}
                    >
                        <div>
                            Author: Noname
                            <br/>
                            Collections Params
                            <br/>
                            Price: 3$
                        </div>
                    </Card>
                </div>
                <div>
                    <Card
                        hoverable
                        style={{
                            width: "90%",
                            cursor: "pointer",
                            marginLeft: '10px',
                            marginRight: '10px',
                            marginBottom: '30px'
                        }}
                        cover={<img alt="example" src="https://images.unsplash.com/photo-1549989476-69a92fa57c36?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=800&q=60"/>}
                    >
                        <div>
                            Author: Noname
                            <br/>
                            Collections Params
                            <br/>
                            Price: 3$
                        </div>
                    </Card></div>
            </Carousel>
        </div>
    )
}
