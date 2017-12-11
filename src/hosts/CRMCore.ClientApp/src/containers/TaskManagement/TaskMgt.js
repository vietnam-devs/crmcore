import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { Actions } from 'jumpstate';

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
  Button,
  DropdownMenu,
  DropdownItem
} from 'reactstrap';
import classnames from 'classnames';

import { StandardPanel, PageHeader } from 'components';
import { taskSelectors } from 'redux/modules/task';
import {
  NotStartedPanel,
  InProgressPanel,
  PendingPanel,
  DonePanel
} from './TaskStatusPanel';

class AdvancedSearchPanel extends PureComponent {
  constructor(props) {
    super(props);
    this.state = { activeTab: '1' };
  }

  tabToggle = tab => {
    if (this.state.activeTab !== tab) {
      this.setState({
        activeTab: tab
      });
    }
  };

  render() {
    return (
      <div>
        <div className="form-group">
          <label className="form-control-label">Assigned User</label>
          <div>
            <input type="text" className="form-control" />{' '}
          </div>
        </div>
        <br />
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
              Status
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
              Category
            </NavLink>
          </NavItem>
        </Nav>
        <TabContent activeTab={this.state.activeTab}>
          <TabPane tabId="1">
            <Row>
              <Col sm="6">
                <ListGroup>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" defaultChecked /> &nbsp;Not started{' '}
                    <Badge pill className="pull-right">
                      14
                    </Badge>
                  </ListGroupItem>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" /> &nbsp;In progress{' '}
                    <Badge pill className="pull-right">
                      2
                    </Badge>
                  </ListGroupItem>
                </ListGroup>
              </Col>
              <Col sm="6">
                <ListGroup>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" defaultChecked /> &nbsp;Pending{' '}
                    <Badge pill className="pull-right">
                      1
                    </Badge>
                  </ListGroupItem>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" /> &nbsp;Done{' '}
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
                    <input type="checkbox" /> &nbsp;Call{' '}
                    <Badge pill className="pull-right">
                      14
                    </Badge>
                  </ListGroupItem>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" defaultChecked /> &nbsp;Email{' '}
                    <Badge pill className="pull-right">
                      2
                    </Badge>
                  </ListGroupItem>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" defaultChecked /> &nbsp;Follow Up{' '}
                    <Badge pill className="pull-right">
                      1
                    </Badge>
                  </ListGroupItem>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" /> &nbsp;Lunch{' '}
                    <Badge pill className="pull-right">
                      1
                    </Badge>
                  </ListGroupItem>
                </ListGroup>
              </Col>
              <Col sm="6">
                <ListGroup>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" defaultChecked /> &nbsp;Meeting{' '}
                    <Badge pill className="pull-right">
                      1
                    </Badge>
                  </ListGroupItem>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" /> &nbsp;Money{' '}
                    <Badge pill className="pull-right">
                      1
                    </Badge>
                  </ListGroupItem>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" defaultChecked /> &nbsp;Presentation{' '}
                    <Badge pill className="pull-right">
                      1
                    </Badge>
                  </ListGroupItem>
                  <ListGroupItem className="justify-content-between">
                    <input type="checkbox" defaultChecked /> &nbsp;Trip{' '}
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
          <p className="text-muted">Total tasks have found: 100</p>
        </div>
      </div>
    );
  }
}

class StandardSearchPanel extends PureComponent {
  constructor(props) {
    super(props);
    this.state = { collapse: true };
  }

  collapse = e => {
    e.preventDefault();
    this.setState({ collapse: !this.state.collapse });
  };

  render() {
    return (
      <StandardPanel icon="icon-magic-wand" title="Search" collapse={true}>
        <div className="form-group">
          <label className="form-control-label">Task Name</label>
          <div>
            <input type="text" className="form-control" />{' '}
          </div>
        </div>

        <div className="form-group">
          <a className="text-muted" href="#" onClick={this.collapse}>
            Advanced search
          </a>
        </div>

        {!this.state.collapse && <AdvancedSearchPanel />}
      </StandardPanel>
    );
  }
}

class StatisticPanel extends PureComponent {
  render() {
    return (
      <StandardPanel
        title="Statistic"
        showMaximize={true}
        showIcon={false}
        collapse={true}
      >
        <Row>
          <Col>
            <h5>Current status.</h5>
            <p>Task (Not started): 100</p>
            <p>Task (In progress): 2</p>
            <p>Task (Pending): 10</p>
            <p>Task (Done): 200</p>
          </Col>
          <Col>
            <h5>10 latest tasks updated.</h5>
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
                  <a href="#">See all...</a>
                </div>
              </ListGroupItem>
            </ListGroup>
          </Col>
        </Row>
      </StandardPanel>
    );
  }
}

class TaskMgt extends PureComponent {
  componentDidMount() {
    Actions.tasks.loadCategoryTypes();
    Actions.tasks.loadStatuses();
    Actions.tasks.loadTasksByStatus();
  }

  createTask = () => {
    this.props.history.push('/crm/task-form/$');
  };

  render() {
    const mainActions = (
      <Button color="primary" onClick={this.createTask}>
        <i className="fa fa-plus" aria-hidden="true" /> Add
      </Button>
    );

    const dropdownMenus = (
      <DropdownMenu>
        <DropdownItem>Not Started Task</DropdownItem>
        <DropdownItem>In Progress Task</DropdownItem>
        <DropdownItem>Pending Task</DropdownItem>
        <DropdownItem>Done Task</DropdownItem>
      </DropdownMenu>
    );

    return (
      <div className="animated fadeIn">
        <PageHeader
          title="Tasks"
          mainActions={mainActions}
          dropdownMenus={dropdownMenus}
        />

        <StatisticPanel />

        <StandardSearchPanel />

        <Row>
          <Col xs="12" sm="6" lg="3">
            <NotStartedPanel
              tasks={this.props.notStartedTasks}
              taskByKeys={this.props.taskByKeys}
              categoryByKeys={this.props.categoryByKeys}
              loading={this.props.loading}
            />
          </Col>
          <Col xs="12" sm="6" lg="3">
            <InProgressPanel
              tasks={this.props.inProgressTasks}
              taskByKeys={this.props.taskByKeys}
              categoryByKeys={this.props.categoryByKeys}
              loading={this.props.loading}
            />
          </Col>
          <Col className="d-md-down-none">
            <PendingPanel
              tasks={this.props.pendingTasks}
              taskByKeys={this.props.taskByKeys}
              categoryByKeys={this.props.categoryByKeys}
              loading={this.props.loading}
            />
          </Col>
          <Col className="d-md-down-none">
            <DonePanel
              tasks={this.props.doneTasks}
              taskByKeys={this.props.taskByKeys}
              categoryByKeys={this.props.categoryByKeys}
              loading={this.props.loading}
            />
          </Col>
        </Row>
      </div>
    );
  }
}

const mapStateToProps = state => ({
  loading: taskSelectors.getLoading(state),
  categoryByKeys: taskSelectors.getCategoryByKeys(state),
  taskByKeys: taskSelectors.getTaskByKeys(state),
  notStartedTasks: taskSelectors.getNotStartedTasks(state),
  inProgressTasks: taskSelectors.getInProgressTasks(state),
  pendingTasks: taskSelectors.getPendingTasks(state),
  doneTasks: taskSelectors.getDoneTasks(state)
});

export default connect(mapStateToProps)(TaskMgt);
