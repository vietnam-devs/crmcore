import React, { Component } from 'react';

import {
  Row,
  Col,
  Nav,
  NavItem,
  NavLink,
  TabContent,
  TabPane,
  ListGroup,
  ListGroupItem,
  Badge,
  DropdownMenu,
  DropdownItem
} from 'reactstrap';
import classnames from 'classnames';

import { SearchPanel, StandardPanel } from 'components';

class SearchPanelWithContent extends Component {
  constructor(props) {
    super(props);
    this.tabToggle = this.tabToggle.bind(this);
    this.state = { activeTab: '1' };
  }

  tabToggle(tab) {
    if (this.state.activeTab !== tab) {
      this.setState({
        activeTab: tab
      });
    }
  }

  render() {
    return (
      <SearchPanel>
        <Nav tabs>
          <NavItem>
            <NavLink
              className={classnames({
                active: this.state.activeTab === '1'
              })}
              onClick={() => {
                this.tabToggle('1');
              }}
            >
              Pending
            </NavLink>
          </NavItem>
          <NavItem>
            <NavLink
              className={classnames({
                active: this.state.activeTab === '2'
              })}
              onClick={() => {
                this.tabToggle('2');
              }}
            >
              Assigned
            </NavLink>
          </NavItem>
          <NavItem>
            <NavLink
              className={classnames({
                active: this.state.activeTab === '3'
              })}
              onClick={() => {
                this.tabToggle('3');
              }}
            >
              Completed
            </NavLink>
          </NavItem>
        </Nav>
        <TabContent activeTab={this.state.activeTab}>
          <TabPane tabId="1">
            <Row>
              <Col sm="6">
                <ListGroup>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" defaultChecked /> &nbsp;Overdue{' '}
                    <Badge pill className="pull-right">
                      14
                    </Badge>
                  </ListGroupItem>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" /> &nbsp;As Soon As Possible{' '}
                    <Badge pill className="pull-right">
                      2
                    </Badge>
                  </ListGroupItem>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" defaultChecked /> &nbsp;Today{' '}
                    <Badge pill className="pull-right">
                      1
                    </Badge>
                  </ListGroupItem>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" /> &nbsp;Tomorrow{' '}
                    <Badge pill className="pull-right">
                      1
                    </Badge>
                  </ListGroupItem>
                </ListGroup>
              </Col>
              <Col sm="6">
                <ListGroup>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" /> &nbsp;This Week{' '}
                    <Badge pill className="pull-right">
                      1
                    </Badge>
                  </ListGroupItem>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" defaultChecked /> &nbsp;Next Week{' '}
                    <Badge pill className="pull-right">
                      1
                    </Badge>
                  </ListGroupItem>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" defaultChecked /> &nbsp;Sometime
                    Later{' '}
                    <Badge pill className="pull-right">
                      1
                    </Badge>
                  </ListGroupItem>
                </ListGroup>
              </Col>
            </Row>
          </TabPane>
          <TabPane tabId="2">
            <Row>
              <Col sm="6">
                <ListGroup>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" /> &nbsp;Overdue{' '}
                    <Badge pill className="pull-right">
                      14
                    </Badge>
                  </ListGroupItem>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" defaultChecked /> &nbsp;As Soon As
                    Possible{' '}
                    <Badge pill className="pull-right">
                      2
                    </Badge>
                  </ListGroupItem>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" defaultChecked /> &nbsp;Today{' '}
                    <Badge pill className="pull-right">
                      1
                    </Badge>
                  </ListGroupItem>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" /> &nbsp;Tomorrow{' '}
                    <Badge pill className="pull-right">
                      1
                    </Badge>
                  </ListGroupItem>
                </ListGroup>
              </Col>
              <Col sm="6">
                <ListGroup>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" defaultChecked /> &nbsp;This Week{' '}
                    <Badge pill className="pull-right">
                      1
                    </Badge>
                  </ListGroupItem>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" /> &nbsp;Next Week{' '}
                    <Badge pill className="pull-right">
                      1
                    </Badge>
                  </ListGroupItem>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" defaultChecked /> &nbsp;Sometime
                    Later{' '}
                    <Badge pill className="pull-right">
                      1
                    </Badge>
                  </ListGroupItem>
                </ListGroup>
              </Col>
            </Row>
          </TabPane>
          <TabPane tabId="3">
            <Row>
              <Col sm="6">
                <ListGroup>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" /> &nbsp;Today{' '}
                    <Badge pill className="pull-right">
                      14
                    </Badge>
                  </ListGroupItem>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" defaultChecked /> &nbsp;Yesterday{' '}
                    <Badge pill className="pull-right">
                      2
                    </Badge>
                  </ListGroupItem>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" /> &nbsp;Last week{' '}
                    <Badge pill className="pull-right">
                      1
                    </Badge>
                  </ListGroupItem>
                </ListGroup>
              </Col>
              <Col sm="6">
                <ListGroup>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" /> &nbsp;This month{' '}
                    <Badge pill className="pull-right">
                      1
                    </Badge>
                  </ListGroupItem>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" defaultChecked /> &nbsp;Last month{' '}
                    <Badge pill className="pull-right">
                      1
                    </Badge>
                  </ListGroupItem>
                </ListGroup>
              </Col>
            </Row>
          </TabPane>
        </TabContent>
        <div>
          <p className="text-muted">Total Pending Tasks: 100</p>
        </div>
      </SearchPanel>
    );
  }
}

export default class TaskMgt extends Component {
  constructor(props) {
    super(props);
    this.createTask = this.createTask.bind(this);
  }

  createTask() {
    console.log('Create task...');
    this.props.history.push('/crm/task-form/$');
  }

  render() {
    const actions = (
      <DropdownMenu>
        <DropdownItem onClick={this.createTask}>
          <i className="icon-plus b-icon" />Create Task
        </DropdownItem>
      </DropdownMenu>
    );

    return (
      <div className="animated fadeIn">
        <StandardPanel title="Tasks" actions={actions}>
          <Row>
            <Col>
              <SearchPanelWithContent />
            </Col>
          </Row>
          <Row>
            <Col>
              <h5>Overdue</h5>
              <ListGroup>
                <ListGroupItem className="justify-content-between">
                  <Badge pill className="pull-left">
                    Meeting
                  </Badge>
                  &nbsp;<a href="#">Jacky</a>: Inpsect athletic fields re:{' '}
                  <a href="#">Account 1</a> - 1 day late, was due on Nov 29 at
                  7:00PM
                </ListGroupItem>

                <ListGroupItem>
                  <div className="text-center">
                    <a href="#">Load more...</a>
                  </div>
                </ListGroupItem>
              </ListGroup>
              <hr />
              <h5>As Soon As Possible</h5>
              <ListGroup>
                <ListGroupItem className="justify-content-between">
                  <Badge pill className="pull-left">
                    Meeting
                  </Badge>
                  &nbsp;<a href="#">Jacky</a>: Inpsect athletic fields re:{' '}
                  <a href="#">Account 1</a> - 1 day late, was due on Nov 29 at
                  7:00PM
                </ListGroupItem>

                <ListGroupItem className="justify-content-between">
                  <Badge pill color="success" className="pull-left">
                    Presentation
                  </Badge>
                  &nbsp;<a href="#">Lena</a>: Do a presentation about ReactJS
                  for the team - 2 day late, was due on Nov 22 at 6:30PM
                </ListGroupItem>

                <ListGroupItem>
                  <div className="text-center">
                    <a href="#">Load more...</a>
                  </div>
                </ListGroupItem>
              </ListGroup>
              <hr />
              <h5>Tomorrow</h5>
              <ListGroup>
                <ListGroupItem className="justify-content-between">
                  <Badge pill className="pull-left">
                    Meeting
                  </Badge>
                  &nbsp;<a href="#">Jacky</a>: Inpsect athletic fields re:{' '}
                  <a href="#">Account 1</a> - 1 day late, was due on Nov 29 at
                  7:00PM
                </ListGroupItem>

                <ListGroupItem className="justify-content-between">
                  <Badge pill color="danger" className="pull-left">
                    Follow-up
                  </Badge>
                  &nbsp;<a href="#">Chris</a>: Et exercitationem eaque commodi
                  dolorem tenetur aut re:{' '}
                  <a href="#"> Officiis distinctio est nam illum official</a> -
                  about 2 months late, was due on Oct 10 at 12:00AM
                </ListGroupItem>

                <ListGroupItem>
                  <div className="text-center">
                    <a href="#">Load more...</a>
                  </div>
                </ListGroupItem>
              </ListGroup>
              <hr />
              <h5>Sometime Later</h5>
              <ListGroup>
                <ListGroupItem className="justify-content-between">
                  <Badge pill className="pull-left">
                    Meeting
                  </Badge>
                  &nbsp;<a href="#">Jacky</a>: Inpsect athletic fields re:{' '}
                  <a href="#">Account 1</a> - 1 day late, was due on Nov 29 at
                  7:00PM
                </ListGroupItem>

                <ListGroupItem className="justify-content-between">
                  <Badge pill color="warning" className="pull-left">
                    Lunch
                  </Badge>
                  &nbsp;<a href="#">Lena</a>: Et exercitationem eaque commodi
                  dolorem tenetur aut re:{' '}
                  <a href="#"> Officiis distinctio est nam illum official</a> -
                  about 2 months late, was due on Oct 10 at 12:00AM
                </ListGroupItem>

                <ListGroupItem>
                  <div className="text-center">
                    <a href="#">Load more...</a>
                  </div>
                </ListGroupItem>
              </ListGroup>
            </Col>
          </Row>
        </StandardPanel>
      </div>
    );
  }
}
