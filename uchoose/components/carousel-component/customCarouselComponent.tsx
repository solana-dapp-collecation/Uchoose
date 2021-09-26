import React from "react";
import Section from "./section";
import WithScrollbar from "./withScrollbar";
import Carousel from "react-multi-carousel";
import "react-multi-carousel/lib/styles.css";
import { Card, Tag, Divider, Rate, Button, Menu, Dropdown, Radio } from "antd";
import { createFromIconfontCN, CheckCircleOutlined, ShareAltOutlined, MoneyCollectOutlined } from '@ant-design/icons';

const IconFont = createFromIconfontCN({
  scriptUrl: [
      '//at.alicdn.com/t/font_8d5l8fzk5b87iudi.js',
      '//at.alicdn.com/t/font_1788592_a5xf2bdic3u.js' // icon-shoppingcart
    ]
});

// export function CarouselWithCards(props: React.PropsWithChildren<{}>) {
//     return (<Section>
//         <WithScrollbar/>
//     </Section>)
// }
const {Meta} = Card;

function handleMenuClick(e: any) {
    console.log('click menu item', e);
}

function handleCoCreate(e: any) {
    console.log('TOTO - add form wih choosing parts for new NFT', e);
}

const menu = (
    <Menu onClick={handleMenuClick}>
      <Menu.Item key="facebook"><IconFont type="icon-facebook" /> Facebook</Menu.Item>
      <Menu.Item key="twitter"><IconFont type="icon-twitter" /> Twitter</Menu.Item>
    </Menu>
  );

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
    return (
        <div style={{width: '1000px'}}>
            {/* https://www.npmjs.com/package/react-multi-carousel */}
            <Divider orientation="left"><MoneyCollectOutlined /> <a href="https://opensea.io/collection/pixeldoges"><b>PixelDoges</b></a></Divider>
            
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
                        cover={<img alt="example" src="/Pixel-Doge-3916.png"/>}
                    >
                        <Meta title="Pixel Doge #3916" description="Generated Dodge 3916" />
                        <br/>
                        <div>
                            <b>Author</b>: <a href="https://opensea.io/kementari">kementari</a>
                            <br/>
                            <b>Co-creator</b>: <a href="https://opensea.io/Milko">Milko</a>
                            <br/>
                            <b>Owner</b>: -
                            <Divider>Properties</Divider>
                            <Tag color="green">undershirt</Tag>
                            <Tag color="brown">hat</Tag>
                            <Tag>crying face</Tag>
                            <Divider>Price</Divider>
                            <b>Price</b>: 1<img alt="example" src="/eth.svg" style={{marginLeft: '2px', width: '10px'}}/>
                            <Divider>Rating</Divider>
                            <Rate allowHalf defaultValue={2.5} /> 2.5
                            <Divider/>
                            <>
                                <Button type="primary" value="buy"><IconFont type="icon-shoppingcart" /> Buy</Button>
                                <Button type="primary" value="co-create" style={{marginLeft: '5px'}} onClick={handleCoCreate}><CheckCircleOutlined /> Co-create</Button>
                                <br/><br/>
                                <Dropdown.Button overlay={menu}><ShareAltOutlined /> Share</Dropdown.Button>
                            </>
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
                        cover={<img alt="example" src="/Pixel-Doge-4954.png"/>}
                    >
                        <Meta title="Pixel Doge #4954" description="Generated Dodge 4954" />
                        <br/>
                        <div>
                            <b>Author</b>: <a href="https://opensea.io/kementari">kementari</a>
                            <br/>
                            <b>Co-creator</b>: <a href="https://opensea.io/BoundTwo">BoundTwo</a>
                            <br/>
                            <b>Owner</b>: -
                            <Divider>Properties</Divider>
                            <Tag color="purple">sweater</Tag>
                            <Tag color="green">cap</Tag>
                            <Tag>face with one eye</Tag>
                            <Divider>Price</Divider>
                            <b>Price</b>: 2<img alt="example" src="/eth.svg" style={{marginLeft: '2px', width: '10px'}}/>
                            <Divider>Rating</Divider>
                            <Rate allowHalf defaultValue={3.5} /> 3.5
                            <Divider/>
                            <>
                                <Button type="primary" value="buy"><IconFont type="icon-shoppingcart" /> Buy</Button>
                                <Button type="primary" value="co-create" style={{marginLeft: '5px'}} onClick={handleCoCreate}><CheckCircleOutlined /> Co-create</Button>
                                <br/><br/>
                                <Dropdown.Button overlay={menu}><ShareAltOutlined /> Share</Dropdown.Button>
                            </>
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
                        cover={<img alt="example" src="/Pixel-Doge-1604.png"/>}
                    >
                        <Meta title="Pixel Doge #1604" description="Generated Dodge 1604" />
                        <br/>
                        <div>
                            <b>Author</b>: <a href="https://opensea.io/kementari">kementari</a>
                            <br/>
                            <b>Co-creator</b>: <a href="https://opensea.io/Milko">Milko</a>
                            <br/>
                            <b>Owner</b>: -
                            <Divider>Properties</Divider>
                            <Tag color="red">scarf</Tag>
                            <Tag color="black">iroquois</Tag>
                            <Tag>crying face</Tag>
                            <Divider>Price</Divider>
                            <b>Price</b>: 3<img alt="example" src="/eth.svg" style={{marginLeft: '2px', width: '10px'}}/>
                            <Divider>Rating</Divider>
                            <Rate allowHalf defaultValue={4} /> 4.0
                            <Divider/>
                            <>
                                <Button type="primary" value="buy"><IconFont type="icon-shoppingcart" /> Buy</Button>
                                <Button type="primary" value="co-create" style={{marginLeft: '5px'}} onClick={handleCoCreate}><CheckCircleOutlined /> Co-create</Button>
                                <br/><br/>
                                <Dropdown.Button overlay={menu}><ShareAltOutlined /> Share</Dropdown.Button>
                            </>
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
                        cover={<img alt="example" src="/Pixel-Doge-715.png"/>}
                    >
                        <Meta title="Pixel Doge #715" description="Generated Dodge 715" />
                        <br/>
                        <div>
                            <b>Author</b>: <a href="https://opensea.io/kementari">kementari</a>
                            <br/>
                            <b>Co-creator</b>: <a href="https://opensea.io/BigOunce">BigOunce</a>
                            <br/>
                            <b>Owner</b>: -
                            <Divider>Properties</Divider>
                            <Tag color="blue">undershirt</Tag>
                            <Tag color="purple">cap</Tag>
                            <Tag>sleepy face</Tag>
                            <Divider>Price</Divider>
                            <b>Price</b>: 2<img alt="example" src="/eth.svg" style={{marginLeft: '2px', width: '10px'}}/>
                            <Divider>Rating</Divider>
                            <Rate allowHalf defaultValue={3.5} /> 3.5
                            <Divider/>
                            <>
                                <Button type="primary" value="buy"><IconFont type="icon-shoppingcart" /> Buy</Button>
                                <Button type="primary" value="co-create" style={{marginLeft: '5px'}} onClick={handleCoCreate}><CheckCircleOutlined /> Co-create</Button>
                                <br/><br/>
                                <Dropdown.Button overlay={menu}><ShareAltOutlined /> Share</Dropdown.Button>
                            </>
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
                        cover={<img alt="example" src="/Pixel-Doge-5481.png"/>}
                    >
                        <Meta title="Pixel Doge #5481" description="Generated Dodge 5481" />
                        <br/>
                        <div>
                            <b>Author</b>: <a href="https://opensea.io/kementari">kementari</a>
                            <br/>
                            <b>Co-creator</b>: <a href="https://opensea.io/ThanosWon">ThanosWon</a>
                            <br/>
                            <b>Owner</b>: -
                            <Divider>Properties</Divider>
                            <Tag color="green">undershirt</Tag>
                            <Tag color="red">horns</Tag>
                            <Tag>domino face</Tag>
                            <Divider>Price</Divider>
                            <b>Price</b>: 2<img alt="example" src="/eth.svg" style={{marginLeft: '2px', width: '10px'}}/>
                            <Divider>Rating</Divider>
                            <Rate allowHalf defaultValue={3} /> 3.0
                            <Divider/>
                            <>
                                <Button type="primary" value="buy"><IconFont type="icon-shoppingcart" /> Buy</Button>
                                <Button type="primary" value="co-create" style={{marginLeft: '5px'}} onClick={handleCoCreate}><CheckCircleOutlined /> Co-create</Button>
                                <br/><br/>
                                <Dropdown.Button overlay={menu}><ShareAltOutlined /> Share</Dropdown.Button>
                            </>
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
                        cover={<img alt="example" src="/Pixel-Doge-5154.png"/>}
                    >
                        <Meta title="Pixel Doge #5154" description="Generated Dodge 5154" />
                        <br/>
                        <div>
                            <b>Author</b>: <a href="https://opensea.io/kementari">kementari</a>
                            <br/>
                            <b>Co-creator</b>: <a href="https://opensea.io/Elchigro">Elchigro</a>
                            <br/>
                            <b>Owner</b>: -
                            <Divider>Properties</Divider>
                            <Tag color="purple">cravat</Tag>
                            <Tag color="brown">arrow</Tag>
                            <Tag>face with tongue</Tag>
                            <Divider>Price</Divider>
                            <b>Price</b>: 2<img alt="example" src="/eth.svg" style={{marginLeft: '2px', width: '10px'}}/>
                            <Divider>Rating</Divider>
                            <Rate allowHalf defaultValue={2.5} /> 2.5
                            <Divider/>
                            <>
                                <Button type="primary" value="buy"><IconFont type="icon-shoppingcart" /> Buy</Button>
                                <Button type="primary" value="co-create" style={{marginLeft: '5px'}} onClick={handleCoCreate}><CheckCircleOutlined /> Co-create</Button>
                                <br/><br/>
                                <Dropdown.Button overlay={menu}><ShareAltOutlined /> Share</Dropdown.Button>
                            </>
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
                        cover={<img alt="example" src="/Pixel-Doge-598.png"/>}
                    >
                        <Meta title="Pixel Doge #598" description="Generated Dodge 598" />
                        <br/>
                        <div>
                            <b>Author</b>: <a href="https://opensea.io/kementari">kementari</a>
                            <br/>
                            <b>Co-creator</b>: <a href="https://opensea.io/digitalartdealer">digitalartdealer</a>
                            <br/>
                            <b>Owner</b>: -
                            <Divider>Properties</Divider>
                            <Tag color="blue">sweater</Tag>
                            <Tag color="black">iroquois</Tag>
                            <Tag>war face</Tag>
                            <Divider>Price</Divider>
                            <b>Price</b>: 2<img alt="example" src="/eth.svg" style={{marginLeft: '2px', width: '10px'}}/>
                            <Divider>Rating</Divider>
                            <Rate allowHalf defaultValue={3.2} /> 3.2
                            <Divider/>
                            <>
                                <Button type="primary" value="buy"><IconFont type="icon-shoppingcart" /> Buy</Button>
                                <Button type="primary" value="co-create" style={{marginLeft: '5px'}} onClick={handleCoCreate}><CheckCircleOutlined /> Co-create</Button>
                                <br/><br/>
                                <Dropdown.Button overlay={menu}><ShareAltOutlined /> Share</Dropdown.Button>
                            </>
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
                        cover={<img alt="example" src="/Pixel-Doge-5447.png"/>}
                    >
                        <Meta title="Pixel Doge #5447" description="Generated Dodge 5447" />
                        <br/>
                        <div>
                            <b>Author</b>: <a href="https://opensea.io/kementari">kementari</a>
                            <br/>
                            <b>Co-creator</b>: <a href="https://opensea.io/Milko">Milko</a>
                            <br/>
                            <b>Owner</b>: -
                            <Divider>Properties</Divider>
                            <Tag color="red">undershirt</Tag>
                            <Tag color="yellow">cap</Tag>
                            <Tag>face with tongue</Tag>
                            <Divider>Price</Divider>
                            <b>Price</b>: 2<img alt="example" src="/eth.svg" style={{marginLeft: '2px', width: '10px'}}/>
                            <Divider>Rating</Divider>
                            <Rate allowHalf defaultValue={4.1} /> 4.1
                            <Divider/>
                            <>
                                <Button type="primary" value="buy"><IconFont type="icon-shoppingcart" /> Buy</Button>
                                <Button type="primary" value="co-create" style={{marginLeft: '5px'}} onClick={handleCoCreate}><CheckCircleOutlined /> Co-create</Button>
                                <br/><br/>
                                <Dropdown.Button overlay={menu}><ShareAltOutlined /> Share</Dropdown.Button>
                            </>
                        </div>
                    </Card>
                </div>
            </Carousel>
        </div>
    )
}
