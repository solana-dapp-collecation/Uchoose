import React from "react";
import Section from "./section";
import WithScrollbar from "./withScrollbar";
import Carousel from "react-multi-carousel";
import "react-multi-carousel/lib/styles.css";

// export function CarouselWithCards(props: React.PropsWithChildren<{}>) {
//     return (<Section>
//         <WithScrollbar/>
//     </Section>)
// }

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
                autoPlaySpeed={3000}
                keyBoardControl={true}
                customTransition="all .5"
                transitionDuration={600}
                containerClass="carousel-container"
                removeArrowOnDeviceType={["tablet", "mobile"]}
                //   deviceType={this.props.deviceType}
                dotListClass="custom-dot-list-style"
                itemClass="carousel-item-padding-40-px"
            >
                <div><img
                    draggable={false}
                    style={{
                        width: "90%",
                        cursor: "pointer",
                        marginLeft: '10px',
                        marginRight: '10px',
                        marginBottom: '30px'
                    }}
                    src="https://images.unsplash.com/photo-1549989476-69a92fa57c36?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=800&q=60"
                />
                </div>
                <div><img
                    draggable={false}
                    style={{
                        width: "90%",
                        cursor: "pointer",
                        marginLeft: '10px',
                        marginRight: '10px',
                        marginBottom: '30px'
                    }}
                    src="https://images.unsplash.com/photo-1549989476-69a92fa57c36?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=800&q=60"
                /></div>
                <div><img
                    draggable={false}
                    style={{
                        width: "90%",
                        cursor: "pointer",
                        marginLeft: '10px',
                        marginRight: '10px',
                        marginBottom: '30px'
                    }}
                    src="https://images.unsplash.com/photo-1549989476-69a92fa57c36?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=800&q=60"
                /></div>
                <div><img
                    draggable={false}
                    style={{
                        width: "90%",
                        cursor: "pointer",
                        marginLeft: '10px',
                        marginRight: '10px',
                        marginBottom: '30px'
                    }}
                    src="https://images.unsplash.com/photo-1549989476-69a92fa57c36?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=800&q=60"
                /></div>
            </Carousel>
        </div>
    )
}
